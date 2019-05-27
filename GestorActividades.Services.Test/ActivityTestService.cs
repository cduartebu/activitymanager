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
    public class ActivityTestService
    {
        private IUnitOfWorkFactory myUnitOfWorkFactory;

        private IUnitOfWork myUnitOfWork;

        private IGenericRepository<Activity> myActivityRepository;
        private IGenericRepository<Team> myTeamRepository;

        [TestInitialize]
        public void SetUp()
        {
            myUnitOfWorkFactory = MockRepository.GenerateMock<IUnitOfWorkFactory>();
            myUnitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
            myActivityRepository = MockRepository.GenerateMock<IGenericRepository<Activity>>();
            myTeamRepository = MockRepository.GenerateMock<IGenericRepository<Team>>();
        }

        [TestMethod]
        public void AddActivity_WhenActivityDescriptionIsMissing()
        {
            //Arrange
            var newActivity = new Activity { Description = string.Empty };

            var activityService = new ActivityService();

            //Act

            var result = activityService.AddActivity(newActivity);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The Description field is required.", result.StatusMessage);
        }

        [TestMethod]
        public void UpdateActivity_WhenTheTeamDoesntExist()
        {
            //Arrange
            myActivityRepository.Expect(x => x.GetAll()).Return(new List<Activity> { new Activity { ActivityId = 1, Description="Activity 1" } }.AsQueryable()).Repeat.Once();
            myTeamRepository.Expect(x => x.GetAll()).Return(new List<Team> { new Team { TeamId = 5, TeamName = "TestTeam" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Activity>()).Return(myActivityRepository).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Team>()).Return(myTeamRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newActivity = new Activity { ActivityId = 1, Description = "Activity 1.1", TeamId = 1 };

            var ActivityService = new ActivityService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = ActivityService.UpdateActivity(newActivity);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The team doesn't exist in the system.", result.StatusMessage);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myActivityRepository.VerifyAllExpectations();
            myTeamRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetActivityById_WhenTheActivityDoesNotExist()
        {
            //Arrange
            myActivityRepository.Expect(x => x.GetAll()).Return(new List<Activity> { new Activity { ActivityId = 1, Description = "Activity 1" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Activity>()).Return(myActivityRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var activityService = new ActivityService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = activityService.GetActivityById(56);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.IsNull(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myActivityRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetActivityById_WhenTheActivityExists()
        {
            //Arrange
            myActivityRepository.Expect(x => x.GetAll()).Return(new List<Activity>
            {
                new Activity { ActivityId = 1, Description="Activity 1", TeamId=5 } ,
                new Activity { ActivityId = 2,Description="Activity 2", TeamId=5  }
            }.AsQueryable()).Repeat.Once();

            myUnitOfWork.Expect(x => x.GetGenericRepository<Activity>()).Return(myActivityRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var ActivityService = new ActivityService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = ActivityService.GetActivityById(2);

            //Asserts
            Assert.AreEqual(StatusCode.Successful, result.StatusCode);
            Assert.AreEqual(2, result.Data.ActivityId);
            Assert.AreEqual("Activity 2", result.Data.Description);
            Assert.AreEqual(5, result.Data.TeamId);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myActivityRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void DeleteActivity_WhenTheActivityDoesNotExist()
        {
            //Arrange
            myActivityRepository.Expect(x => x.GetAll()).Return(new List<Activity> { new Activity { ActivityId = 1, Description = "Activity 1" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Activity>()).Return(myActivityRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var ActivityService = new ActivityService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = ActivityService.DeleteActivity(56);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.IsFalse(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myActivityRepository.VerifyAllExpectations();
        }

    }
}
