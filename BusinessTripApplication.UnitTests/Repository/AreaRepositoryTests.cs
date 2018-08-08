using BusinessTripModels.Models;
using BusinessTripApplication.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BusinessTripApplication.UnitTests.Repository
{
    [TestClass]
    public class AreaRepositoryTests
    {
        [TestMethod]
        public void FindAll_ReturnsList()
        {
            //Arrange
            IList<Area> areas = new List<Area>()
            {
                new Area(),
                new Area()
            };

            Mock<IAreaRepository> MockAreaRepository = new Mock<IAreaRepository>();
            AreaRepositorySetupMoq.FindAll(MockAreaRepository, areas);
            IAreaRepository areaRepository = MockAreaRepository.Object;

            //Act
            IList<Area> findedTrips = areaRepository.FindAll();

            //Assert
            Assert.AreEqual(findedTrips.Count, areas.Count);
            Assert.AreEqual(findedTrips.Count, 2);
        }
    }
}
