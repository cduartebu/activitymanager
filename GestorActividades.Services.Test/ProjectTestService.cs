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
    }
}
