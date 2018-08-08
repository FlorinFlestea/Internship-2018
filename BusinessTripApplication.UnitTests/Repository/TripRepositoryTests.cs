using BusinessTripModels.Models;
using BusinessTripApplication.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BusinessTripApplication.UnitTests.Repository
{
    [TestClass]
    public class TripRepositoryTests
    {
        [TestMethod]
        public void Add_Any_Modify()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>();

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Add(MockTripRepository, trips);
            TripRepositorySetupMoq.GetAll(MockTripRepository, trips);
            ITripRepository tripRepository = MockTripRepository.Object;

            //Act
            Trip trip = new Trip();
            Trip result = tripRepository.Add(trip);
            IList<Trip> findedTrips = tripRepository.GetAll();

            //Assert
            Assert.AreEqual(result, trip);
            Assert.AreEqual(findedTrips.Count, trips.Count);
        }

        [TestMethod]
        public void FindAll_ReturnsList()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(),
                new Trip()
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.GetAll(MockTripRepository, trips);
            ITripRepository tripRepository = MockTripRepository.Object;

            //Act
            IList<Trip> findedTrips = tripRepository.GetAll();

            //Assert
            Assert.AreEqual(findedTrips.Count, trips.Count);
        }

        [TestMethod]
        public void Update_GoodTrip_UpdateTrip()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(){Id = 1, ClientName = "Gigi"},
                new Trip(){Id = 2}
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Update(MockTripRepository, trips);
            TripRepositorySetupMoq.GetAll(MockTripRepository, trips);
            ITripRepository tripRepository = MockTripRepository.Object;

            //Act
            Trip trip = new Trip() { Id = 1, ClientName = "Alin" };
            Trip result = tripRepository.Update(trip);
            IList<Trip> findedTrips = tripRepository.GetAll();

            //Assert
            Assert.AreEqual(result, trip);
            Assert.AreEqual(findedTrips.Count, trips.Count);
            Assert.AreEqual(findedTrips[1], trips[1]);
            Assert.AreEqual(trips[1], trip);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Update_BadTrip_ThrowException()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(){Id = 1, ClientName = "Gigi"},
                new Trip(){Id = 2}
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Update(MockTripRepository, trips);
            TripRepositorySetupMoq.GetAll(MockTripRepository, trips);
            ITripRepository tripRepository = MockTripRepository.Object;

            //Act
            Trip trip = new Trip() { Id = 3, ClientName = "Alin" };

            //Assert
            Trip result = tripRepository.Update(trip);
        }

        [TestMethod]
        public void Remove_GoodTrip_RemoveTrip()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(){Id = 1, ClientName = "Gigi"},
                new Trip(){Id = 2}
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Remove(MockTripRepository, trips);
            TripRepositorySetupMoq.GetAll(MockTripRepository, trips);
            ITripRepository tripRepository = MockTripRepository.Object;

            //Act
            Trip trip = new Trip() { Id = 1, ClientName = "Alin" };
            tripRepository.Remove(trip);
            IList<Trip> findedTrips = tripRepository.GetAll();

            //Assert
            Assert.AreEqual(findedTrips.Count, trips.Count);
            Assert.AreEqual(trips.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Remove_BadTrip_ThrowException()
        {
            //Arrange
            IList<Trip> trips = new List<Trip>()
            {
                new Trip(){Id = 1, ClientName = "Gigi"},
                new Trip(){Id = 2}
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Remove(MockTripRepository, trips);
            TripRepositorySetupMoq.GetAll(MockTripRepository, trips);
            ITripRepository tripRepository = MockTripRepository.Object;

            //Act
            Trip trip = new Trip() { Id = 3, ClientName = "Alin" };

            //Assert
            tripRepository.Remove(trip);
        }
    }
}
