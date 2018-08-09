using BusinessTripModels.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using BusinessTripApplication.UnitTests.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BusinessTripApplication.UnitTests.Service
{
    [TestClass]
    public class AreaServiceTests
    {
        [TestMethod]
        public void FindAll_RetunsList()
        {
            //Arrange
            IList<Area> areas = new List<Area>()
            {
                new Area(),
                new Area()
            };

            Mock<IAreaRepository> MockAreaRepository = new Mock<IAreaRepository>();
            AreaRepositorySetupMoq.FindAll(MockAreaRepository, areas);
            IAreaService userService = new AreaService(MockAreaRepository.Object);

            //Act
            IList<Area> findedUsers = userService.FindAll();

            //Assert
            Assert.AreEqual(findedUsers.Count, 2);
            Assert.AreEqual(findedUsers.Count, areas.Count);
        }
    }
}
