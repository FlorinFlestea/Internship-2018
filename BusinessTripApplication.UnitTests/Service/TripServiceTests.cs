using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using BusinessTripApplication.UnitTests.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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

        [TestMethod]
        public void Aprove_GoodUser_ModifyIt()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(){Id = 1, Status=2}
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Update(MockTripRepository, trips);
            ITripService tripService = new TripService(MockTripRepository.Object);

            //Act
            tripService.Aprove(trips[0]);

            //Assert
            Assert.AreEqual(trips[0].Status, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Aprove_BadUser_Exception()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(){Id = 1, Status=2}
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Update(MockTripRepository, trips);
            ITripService tripService = new TripService(MockTripRepository.Object);

            //Assert
            tripService.Aprove(new Trip() { Id = 2});
        }

        [TestMethod]
        public void Deny_GoodUser_ModifyIt()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(){Id = 1, Status=2}
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Update(MockTripRepository, trips);
            ITripService tripService = new TripService(MockTripRepository.Object);

            //Act
            tripService.Deny(trips[0]);

            //Assert
            Assert.AreEqual(trips[0].Status, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Deny_BadUser_Exception()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(){Id = 1, Status=2}
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Update(MockTripRepository, trips);
            ITripService tripService = new TripService(MockTripRepository.Object);

            //Assert
            tripService.Deny(new Trip() { Id = 2 });
        }
    }
}
