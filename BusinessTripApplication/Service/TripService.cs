﻿using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Service
{
    public class TripService : ITripService
    {
        readonly ITripRepository tripRepository;

        public TripService()
        {
            tripRepository = new TripRepository();
        }

        public TripService(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }

        public Trip Add(Trip addedTrip)
        {
            if (addedTrip.ProjectName == "")
                addedTrip.ProjectName = "Not set!";

            if (addedTrip.TaskName == "")
                addedTrip.TaskName = "Not set!";

            addedTrip.Status = 2;

            return tripRepository.Add(addedTrip);

        }

        public void Remove(Trip trip)
        {
            tripRepository.Remove(trip);
        }
    }
}