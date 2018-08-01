using BusinessTripApplication.Models;
using BusinessTripModels;

namespace BusinessTripApplication.Service
{
    public interface ITripService
    {
        Trip Add(Trip addedTrip);
        void Remove(Trip trip);
    }
}