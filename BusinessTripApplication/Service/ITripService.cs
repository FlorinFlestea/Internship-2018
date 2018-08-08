using BusinessTripApplication.Models;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Service
{
    public interface ITripService
    {
        Trip Add(Trip addedTrip);
        void Remove(Trip trip);
    }
}