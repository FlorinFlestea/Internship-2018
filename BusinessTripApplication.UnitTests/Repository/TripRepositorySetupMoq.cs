using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

using Moq;
using System;
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

        public static void FindAll(Mock<ITripRepository> MockUserRepository, IList<Trip> trips)
        {
            MockUserRepository.Setup(mock => mock.FindAll()).Returns(trips);
        }

        public static void Update(Mock<ITripRepository> MockUserRepository, IList<Trip> trips)
        {
            MockUserRepository.Setup(mock => mock.Update(It.IsAny<Trip>())).Returns(
                (Trip addedTrip) =>
                {
                    Trip toUpdate = trips.Where(trip => trip.Id == addedTrip.Id).FirstOrDefault();
                    if(toUpdate == default(Trip))
                        throw new Exception("Trip doesn't exists");
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
                    Trip toDelete = trips.Where(trip => trip.Id == deletedTrip.Id).FirstOrDefault();
                    if (toDelete == default(Trip))
                        throw new Exception();
                    trips.Remove(toDelete);
                });
        }
    }
}
