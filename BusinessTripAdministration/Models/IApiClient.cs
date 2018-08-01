using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessTripApplication.Models;

namespace BusinessTripAdministration.Models
{
    public interface IApiClient
    {

        Task<IEnumerable<Trip>> GetAllTrips();
        Task<IEnumerable<Trip>> GetApprovedTrips();
        Task<IEnumerable<Trip>> GetDeniedTrips();
        Task<IEnumerable<Trip>> GetPendingTrips();
        Task<Trip> GetTripById(int id);
        void UpdateTrip(int id, Trip trip);
        void DeleteTrip(int id);
        void AddTrip(Trip trip);



    }
}
