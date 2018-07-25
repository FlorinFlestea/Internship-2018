
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

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

            try
            {
                return tripRepository.Add(addedTrip);
            }
            catch 
            {
                throw;
            }
        }

        public void Aprove(Trip trip)
        {
            trip.Status = 1;
            try
            {
                tripRepository.Update(trip);
            }
            catch
            {
                throw;
            }
        }

        public void Remove(Trip trip)
        {
            try
            {
                tripRepository.Remove(trip);
            }
            catch
            {
                throw;
            }
        }

        public void Deny(Trip trip)
        {
            trip.Status = 0;
            try
            {
                tripRepository.Update(trip);
            }
            catch
            {
                throw;
            }
        }
    }
}