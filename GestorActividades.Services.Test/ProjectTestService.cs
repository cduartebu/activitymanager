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
    public class ProjectTestService
    {
        private IUnitOfWorkFactory myUnitOfWorkFactory;

        private IUnitOfWork myUnitOfWork;

        private IGenericRepository<Project> myProjectRepository;

        [TestInitialize]
        public void SetUp()
        {
            myUnitOfWorkFactory = MockRepository.GenerateMock<IUnitOfWorkFactory>();
            myUnitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
            myProjectRepository = MockRepository.GenerateMock<IGenericRepository<Project>>();
        }

        [TestMethod]
        public void AddProject_WhenTheDescriptionIsMissing()
        {
            //Arrange
            var newProyect = new Project { ProjectName = "ddd", Description = "" };

            var proyectService = new ProjectService();

            //Act
            var result = proyectService.AddProject(newProyect);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The Description field is required. ", result.StatusMessage);
        }

        [TestMethod]
        public void AddProject_WhenTheProjectNameIsMissing()
        {
            //Arrange
            var newProyect = new Project { ProjectName = "", Description = "asdasd" };

            var proyectService = new ProjectService();

            //Act
            var result = proyectService.AddProject(newProyect);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The ProjectName field is required. ", result.StatusMessage);
        }

        [TestMethod]
        public void AddProject_WhenTheProjectNameAlreadyExists()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project> { new Project { ProjectName = "test" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newProyect = new Project { ProjectName = "test", Description = "asdasd" };

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act
            var result = proyectService.AddProject(newProyect);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The project name already exits in the system.", result.StatusMessage);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void AddProject_WhenTheModelIsValid_ThenTheProjectIsCreated()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project> { new Project { ProjectName = "test_1" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newProyect = new Project { ProjectName = "test", Description = "asdasd", EndDate = new DateTime(2020, 5, 27), StartDate = new DateTime(2019, 5, 27) };

            myProjectRepository.Expect(x => x.InsertAndSave(newProyect)).Repeat.Once();

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = proyectService.AddProject(newProyect);

            //Asserts
            Assert.AreEqual(StatusCode.Successful, result.StatusCode);
            Assert.AreEqual(newProyect, result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetProjectById_WhenTheProjectDoesNotExist()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project> { new Project { ProjectId = 3, ProjectName = "test_1" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = proyectService.GetProjectById(56);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.IsNull(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetProjectById_WhenTheProjectExists()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project>
            {
                new Project { ProjectId = 3, ProjectName = "test_1", Description = "Project description" } ,
                new Project { ProjectId = 8, ProjectName = "test_8", Description = "Project description 8" }
            }.AsQueryable()).Repeat.Once();

            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = proyectService.GetProjectById(8);

            //Asserts
            Assert.AreEqual(StatusCode.Successful, result.StatusCode);
            Assert.AreEqual(8, result.Data.ProjectId);
            Assert.AreEqual("test_8", result.Data.ProjectName);
            Assert.AreEqual("Project description 8", result.Data.Description);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void UpdateProject_WhenTheModelIsValid_ThenTheProjectIsUpdated()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project>
            {
                new Project { ProjectId = 3, ProjectName = "test_1", Description = "Project description" } ,
                new Project { ProjectId = 8, ProjectName = "test_8", Description = "Project description 8" }
            }.AsQueryable()).Repeat.Twice();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newProyect = new Project
            {
                ProjectId = 3,
                ProjectName = "test",
                Description = "asdasd",
                EndDate = new DateTime(2020, 5, 27),
                StartDate = new DateTime(2019, 5, 27)
            };

            myProjectRepository.Expect(x => x.UpdateAndSave(Arg<Project>.Matches(z => z.ProjectId == 3 && z.ProjectName == "test" && z.Description == "asdasd"))).Repeat.Once();

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = proyectService.UpdateProject(newProyect);

            //Asserts
            Assert.AreEqual(StatusCode.Successful, result.StatusCode);
            Assert.AreEqual(3, result.Data.ProjectId);
            Assert.AreEqual("test", result.Data.ProjectName);
            Assert.AreEqual("asdasd", result.Data.Description);            

            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }


        [TestMethod]
        public void UpdateProject_WhenTheProjectNameAlreadyExists()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project>
            {
                new Project { ProjectId = 3, ProjectName = "test_1", Description = "Project description" } ,
                new Project { ProjectId = 8, ProjectName = "test_8", Description = "Project description 8" }
            }.AsQueryable()).Repeat.Twice();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newProyect = new Project { ProjectId = 3, ProjectName = "test_8", Description = "asdasd" };

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act
            var result = proyectService.UpdateProject(newProyect);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The project name already exits in the system.", result.StatusMessage);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }


        [TestMethod]
        public void UpdateProject_WhenTheProjectDoesNotExist()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project> { new Project { ProjectId = 3, ProjectName = "test_1" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newProyect = new Project { ProjectId = 56, ProjectName = "test_8", Description = "asdasd" };

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = proyectService.UpdateProject(newProyect);

            //Asserts
            Assert.AreEqual(StatusCode.Warning, result.StatusCode);
            Assert.IsNull(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void DeleteProject_WhenTheProjectDoesNotExist()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project> { new Project { ProjectId = 3, ProjectName = "test_1" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = proyectService.DeleteProject(56);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.IsFalse(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void DeleteProject_WhenTheProjectExists()
        {
            //Arrange
            myProjectRepository.Expect(x => x.GetAll()).Return(new List<Project>
            {
                new Project { ProjectId = 3, ProjectName = "test_1", Description = "Project description" } ,
                new Project { ProjectId = 8, ProjectName = "test_8", Description = "Project description 8" }
            }.AsQueryable()).Repeat.Once();

            myUnitOfWork.Expect(x => x.GetGenericRepository<Project>()).Return(myProjectRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            myProjectRepository.Expect(x => x.Delete(Arg<Project>.Matches( z => z.ProjectId == 8 && z.ProjectName == "test_8" && z.Description == "Project description 8")))
                                .Repeat.Once();

            var proyectService = new ProjectService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act
            var result = proyectService.DeleteProject(8);

            //Asserts
            Assert.AreEqual(StatusCode.Successful, result.StatusCode);
            Assert.IsTrue(result.Data);
            
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myProjectRepository.VerifyAllExpectations();
        }
    }
}
