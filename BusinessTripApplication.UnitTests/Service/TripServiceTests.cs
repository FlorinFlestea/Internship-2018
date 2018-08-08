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
    public class TripServiceTests
    {
        [TestMethod]
        public void Add_Any_AddTrip()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip()
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Add(MockTripRepository, trips);
            ITripService tripService = new TripService(MockTripRepository.Object);

            //Act
            tripService.Add(new Trip());

            //Assert
            Assert.AreEqual(trips.Count, 2);
        }
    }
}
