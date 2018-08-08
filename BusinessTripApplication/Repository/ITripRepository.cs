using System.Collections.Generic;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Repository
{
    public interface ITripRepository
    {
        Trip Add(Trip addedTrip);
        Trip FindById(int? id);
        IList<Trip> GetAll();
        Trip Update(Trip updatedTrip);
        void Remove(Trip deleteTrip);

    }
}