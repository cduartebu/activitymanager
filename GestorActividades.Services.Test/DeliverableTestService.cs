using System;
using GestorActividades.Data.UnitOfWork;
using GestorActividades.Infrastructure.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace GestorActividades.Services.Test
{
    [TestClass]
    public class DeliverableTestService
    {
        private IUnitOfWorkFactory myUnitOfWorkFactory;

        private IUnitOfWork myUnitOfWork;

        private IGenericRepository<Deliverable> myDeliverableRepository;

        [TestInitialize]
        public void SetUp()
        {
            myUnitOfWorkFactory = MockRepository.GenerateMock<IUnitOfWorkFactory>();
            myUnitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
            myDeliverableRepository = MockRepository.GenerateMock<IGenericRepository<Deliverable>>();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
