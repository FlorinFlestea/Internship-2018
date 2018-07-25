using BusinessTripApplication.Models;
using System.Collections.Generic;

namespace BusinessTripApplication.Repository
{
    public interface ITripRepository
    {
        Trip Add(Trip addedTrip);
        IList<Trip> FindAll();
        Trip Update(Trip updatedTrip);
        void Remove(Trip deleteTrip);

    }
}