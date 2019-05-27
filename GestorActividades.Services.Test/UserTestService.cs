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
    public class UserTestService
    {
        private IUnitOfWorkFactory myUnitOfWorkFactory;

        private IUnitOfWork myUnitOfWork;

        private IGenericRepository<User> myUserRepository;
        private IGenericRepository<Team> myTeamRepository;

        [TestInitialize]
        public void SetUp()
        {
            myUnitOfWorkFactory = MockRepository.GenerateMock<IUnitOfWorkFactory>();
            myUnitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
            myUserRepository = MockRepository.GenerateMock<IGenericRepository<User>>();
            myTeamRepository = MockRepository.GenerateMock<IGenericRepository<Team>>();
        }

        [TestMethod]
        public void AddUser_WhenUserNameIsMissing()
        {
            //Arrange
            var newUser = new User { UserName = string.Empty, FirstName = "User", LastName = "Name", EmailAddress = "username@test.com" };

            var userService = new UserService();

            //Act

            var result = userService.AddUser(newUser);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The UserName field is required.", result.StatusMessage);
        }

        [TestMethod]
        public void AddUser_WhenFirstNameIsMissing()
        {
            //Arrange
            var newUser = new User { UserName = "UserName", FirstName = string.Empty, LastName = "Name", EmailAddress = "username@test.com" };

            var userService = new UserService();

            //Act

            var result = userService.AddUser(newUser);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The FirstName field is required.", result.StatusMessage);
        }

        [TestMethod]
        public void AddUser_WhenLastNameIsMissing()
        {
            //Arrange
            var newUser = new User { UserName = "UserName", FirstName = "User", LastName = string.Empty, EmailAddress = "username@test.com" };

            var userService = new UserService();

            //Act

            var result = userService.AddUser(newUser);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The LastName field is required.", result.StatusMessage);
        }

        [TestMethod]
        public void AddUser_WhenEmailAddressIsMissing()
        {
            //Arrange
            var newUser = new User { UserName = "UserName", FirstName = "User", LastName = "Name", EmailAddress=string.Empty };

            var userService = new UserService();

            //Act

            var result = userService.AddUser(newUser);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The EmailAddress field is required.", result.StatusMessage);
        }

        [TestMethod]
        public void AddUser_WhenTheUserNameAlreadyExists()
        {
            //Arrange

            myUserRepository.Expect(x => x.GetAll()).Return(new List<User> { new User { UserName = "UserName" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<User>()).Return(myUserRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newUser = new User { UserName = "UserName", FirstName = "User", LastName = "Name", EmailAddress = "username@test.com" };

            var userService = new UserService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = userService.AddUser(newUser);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The UserName already exists in the system.", result.StatusMessage);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myUserRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void UpdateUser_WhenTheTeamDoesntExist()
        {
            //Arrange
            myUserRepository.Expect(x => x.GetAll()).Return(new List<User> { new User { UserId = 1, UserName = "UserName" } }.AsQueryable()).Repeat.Once();
            myTeamRepository.Expect(x => x.GetAll()).Return(new List<Team> { new Team { TeamId = 5,TeamName="TestTeam" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<User>()).Return(myUserRepository).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<Team>()).Return(myTeamRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var newUser = new User { UserId = 1, UserName = "UserName", FirstName = "User", LastName = "Name", EmailAddress = "username@test.com", TeamId=1 };

            var userService = new UserService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = userService.UpdateUser(newUser);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.AreEqual("The Team doesn't exist in the system.", result.StatusMessage);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myUserRepository.VerifyAllExpectations();
            myTeamRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetUserById_WhenTheUserDoesNotExist()
        {
            //Arrange
            myUserRepository.Expect(x => x.GetAll()).Return(new List<User> { new  User { UserId = 1, UserName = "UserName" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<User>()).Return(myUserRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var userService = new UserService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = userService.GetUserById(56);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.IsNull(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myUserRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetUserById_WhenTheUserExists()
        {
            //Arrange
            myUserRepository.Expect(x => x.GetAll()).Return(new List<User>
            {
                new User { UserId = 1, UserName = "UserName", FirstName = "User", LastName = "Name", EmailAddress = "username@test.com", TeamId=null } ,
                new User { UserId = 2, UserName = "UserName2", FirstName = "User2", LastName = "Name2", EmailAddress = "username2@test.com", TeamId=null }
            }.AsQueryable()).Repeat.Once();

            myUnitOfWork.Expect(x => x.GetGenericRepository<User>()).Return(myUserRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var userService = new UserService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = userService.GetUserById(2);

            //Asserts
            Assert.AreEqual(StatusCode.Successful, result.StatusCode);
            Assert.AreEqual(2, result.Data.UserId);
            Assert.AreEqual("UserName2", result.Data.UserName);
            Assert.AreEqual("username2@test.com", result.Data.EmailAddress);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myUserRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void DeleteUser_WhenTheUserDoesNotExist()
        {
            //Arrange
            myUserRepository.Expect(x => x.GetAll()).Return(new List<User> { new User { UserId = 1, UserName = "UserName" } }.AsQueryable()).Repeat.Once();
            myUnitOfWork.Expect(x => x.GetGenericRepository<User>()).Return(myUserRepository).Repeat.Once();
            myUnitOfWorkFactory.Expect(x => x.GetUnitOfWork()).Return(myUnitOfWork).Repeat.Once();

            var userService = new UserService
            {
                UnitOfWorkFactory = myUnitOfWorkFactory
            };

            //Act

            var result = userService.DeleteUser(56);

            //Asserts
            Assert.AreEqual(StatusCode.Error, result.StatusCode);
            Assert.IsFalse(result.Data);
            myUnitOfWork.VerifyAllExpectations();
            myUnitOfWorkFactory.VerifyAllExpectations();
            myUserRepository.VerifyAllExpectations();
        }
    }
}
