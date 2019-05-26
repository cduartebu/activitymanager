using System;
using GestorActividades.Data.UnitOfWork;
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

        [TestInitialize]
        public void SetUp()
        {
            myUnitOfWorkFactory = MockRepository.GenerateMock<IUnitOfWorkFactory>();
            myUnitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
            myActivityRepository = MockRepository.GenerateMock<IGenericRepository<Activity>>();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
