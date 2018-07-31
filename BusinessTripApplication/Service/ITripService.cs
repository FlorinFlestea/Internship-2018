using BusinessTripApplication.Models;

namespace BusinessTripApplication.Service
{
    public interface ITripService
    {
        Trip Add(Trip addedTrip);
        void Aprove(Trip trip);
        void Deny(Trip trip);
        void Remove(Trip trip);
    }
}