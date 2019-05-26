using System;
using GestorActividades.Data.UnitOfWork;
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

        [TestInitialize]
        public void SetUp()
        {
            myUnitOfWorkFactory = MockRepository.GenerateMock<IUnitOfWorkFactory>();
            myUnitOfWork = MockRepository.GenerateMock<IUnitOfWork>();
            myTeamRepository = MockRepository.GenerateMock<IGenericRepository<Team>>();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
