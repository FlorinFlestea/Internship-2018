using System.Collections.Generic;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Service
{
    public interface ITripService
    {
        Trip Add(Trip addedTrip);
        void Remove(Trip trip);
        IList<Trip> GetAll();
        Trip Update(Trip updatedTrip);
        Trip FindById(int? id);


    }
}