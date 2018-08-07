using BusinessTripModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessTripAdministration.Models
{
    public interface IApiClient
    {
        // GET REQUEST: Returns an IEnumerable with all the trips from the db
        Task<IEnumerable<Trip>> GetAllTrips();
        // GET REQUEST: Returns an IEnumerable with the trips that are approved from the db
        Task<IEnumerable<Trip>> GetApprovedTrips();
        // GET REQUEST: Returns an IEnumerable with the trips that are denined from the db
        Task<IEnumerable<Trip>> GetDeniedTrips();
        // GET REQUEST: Returns an IEnumerable with the trips that are waiting for approval/deny
        Task<IEnumerable<Trip>> GetPendingTrips();
        // GET REQUEST: Returns a trip with a given id from the db
        Task<Trip> GetTripById(int id);
        // PUT REQUEST: updates a trip with a given id
        Task<bool> UpdateTrip(int tripId,Trip newTrip);
        // POST REQUEST: adds a trip to the db
        Task<bool> AddTrip(Trip trip);
        // DELETE REQUEST: deletes a trip with a given id from the db
        Task<bool> DeleteTrip(int id);

    }
}
