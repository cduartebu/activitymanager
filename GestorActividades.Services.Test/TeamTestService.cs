using System;
using System.Collections.Generic;
using System.Linq;
using GestorActividades.Data.UnitOfWork;
using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace GestorActividades.Services.Test
{
    [TestClass]
    public class TeamTestService
    {
        private IUnitOfWorkFactory myUnitOfWorkFactory;

        private IUnitOfWork myUnitOfWork;

        private IGenericRepository<Team> myTeamRepository;
         private IGenericRepository<Project> myProjectRepository;

        [TestInitialize]
        public void SetUp()
        {
            myUnitOfWorkFactory = MockRepository.GenerateMock<IUnitOfWorkFactory>();
            myUnitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
            myTeamRepository = MockRepository.GenerateMock<IGenericRepository<Team>>();
            myProjectRepository = MockRepository.GenerateMock<IGenericRepository<Project>>();
        }
        [TestMethod]
        public void AddTeam_WhenTeamNameIsMissing()
        {
            //Arrange
            var newTeam = new Team { TeamName = string.Empty};

            var teamService = new TeamService();

            //Act

            var result = teamService.AddTeam(newTeam);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The TeamName field is required.", result.StatusMessage);
        }

        [TestMethod]
        public void AddTeam_WhenTheTeamNameAlreadyExists()
        {
            //Arrange

            myTeamRepository.Expect(x => x.GetAll()).Return(new List<Team> { new Team { TeamId=1, TeamName = "TeamName" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Team>()).Return(myTeamRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newTeam = new Team { TeamName = "TeamName"};

            var teamService = new TeamService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = teamService.AddTeam(newTeam);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The team name already exists in the system.", result.StatusMessage);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myTeamRepository.VerifyAllExpectations();
        }


        [TestMethod]
        public void AddTeam_WhenTheProjectDoesntExist()
        {
            //Arrange
            myTeamRepository.Expect(x => x.GetAll()).Return(new List<Team> { new Team { TeamId = 1, TeamName = "TeamName", ProjectId = 5 } }.AsQueryable()).Repeat.Once();
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project> { new Project { ProjectId = 5, ProjectName = "TestProject" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Team>()).Return(myTeamRepository).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newTeam = new Team {  TeamName = "TeamName2", ProjectId = 6 };

            var teamService = new TeamService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = teamService.AddTeam(newTeam);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The project doesn't exist in the system.", result.StatusMessage);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myTeamRepository.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }


        [TestMethod]
        public void UpdateTeam_WhenTheProjectDoesntExist()
        {
            //Arrange
            myTeamRepository.Expect(x => x.GetAll()).Return(new List<Team> { new Team { TeamId = 1, TeamName = "TeamName", ProjectId=5 } }.AsQueryable()).Repeat.Once();
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project> { new Project { ProjectId = 5, ProjectName = "TestProject" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Team>()).Return(myTeamRepository).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newTeam = new Team { TeamId = 1, TeamName = "TeamName2", ProjectId = 6 };

            var teamService = new TeamService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = teamService.UpdateTeam(newTeam);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The project doesn't exist in the system.", result.StatusMessage);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myTeamRepository.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetTeamById_WhenTheTeamDoesNotExist()
        {
            //Arrange
            myTeamRepository.Expect(x => x.GetAll()).Return(new List<Team> { new Team { TeamId = 1, TeamName = "TeamName" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Team>()).Return(myTeamRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var TeamService = new TeamService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = TeamService.GetTeamById(56);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.IsNull(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myTeamRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetTeamById_WhenTheTeamExists()
        {
            //Arrange
            myTeamRepository.Expect(x => x.GetAll()).Return(new List<Team>
            {
                new Team { TeamId = 1, TeamName = "TeamName", ProjectId=5 } ,
                new Team { TeamId = 2, TeamName = "TeamName2", ProjectId=5 }
            }.AsQueryable()).Repeat.Once();

            myUnitOfWork.Expect(x => x.GetGenericRepository<Team>()).Return(myTeamRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var TeamService = new TeamService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = TeamService.GetTeamById(2);

            //Asserts
            Assert.AreEqual(StatusCode.Successful, result.StatusCode);
            Assert.AreEqual(2, result.Data.TeamId);
            Assert.AreEqual("TeamName2", result.Data.TeamName);
            Assert.AreEqual(5, result.Data.ProjectId);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myTeamRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void DeleteTeam_WhenTheTeamDoesNotExist()
        {
            //Arrange
            myTeamRepository.Expect(x => x.GetAll()).Return(new List<Team> { new Team { TeamId = 1, TeamName = "TeamName" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Team>()).Return(myTeamRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var TeamService = new TeamService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = TeamService.DeleteTeam(56);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.IsFalse(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myTeamRepository.VerifyAllExpectations();
        }

    }
}
