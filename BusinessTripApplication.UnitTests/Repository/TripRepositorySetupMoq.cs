using BusinessTripModels.Models;
using BusinessTripApplication.Repository;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BusinessTripApplication.UnitTests.Repository
{
    public static class TripRepositorySetupMoq
    {
        public static void Add(Mock<ITripRepository> MockUserRepository, IList<Trip> trips)
        {
            MockUserRepository.Setup(mock => mock.Add(It.IsAny<Trip>())).Returns(
                (Trip addedTrip) =>
                {
                    trips.Add(addedTrip);
                    return addedTrip;
                });
        }

        public static void GetAll(Mock<ITripRepository> MockUserRepository, IList<Trip> trips)
        {
            MockUserRepository.Setup(mock => mock.GetAll()).Returns(trips);
        }

        public static void Update(Mock<ITripRepository> MockUserRepository, IList<Trip> trips)
        {
            MockUserRepository.Setup(mock => mock.Update(It.IsAny<Trip>())).Returns(
                (Trip addedTrip) =>
                {
                    Trip toUpdate = trips.FirstOrDefault(trip => trip.Id == addedTrip.Id);
                    if (toUpdate == default(Trip))
                        throw new System.Exception("Trip doesn't exists");
                    trips.Remove(toUpdate);
                    trips.Add(addedTrip);
                    return addedTrip;
                });
        }

        public static void Remove(Mock<ITripRepository> MockUserRepository, IList<Trip> trips)
        {
            MockUserRepository.Setup(mock => mock.Remove(It.IsAny<Trip>())).Callback(
                (Trip deletedTrip) =>
                {
                    Trip toDelete = trips.FirstOrDefault(trip => trip.Id == deletedTrip.Id);
                    if (toDelete == default(Trip))
                        throw new System.Exception();
                    trips.Remove(toDelete);
                });
        }
    }
}
