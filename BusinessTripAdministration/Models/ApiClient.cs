using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessTripApplication.Models;

namespace BusinessTripAdministration.Models
{
    public class ApiClient : IApiClient
    {
        private readonly string requestLink = "https://localhost:44328/api/tripsapi/";


        public IEnumerable<Trip> GetAllTrips()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetApprovedTrips()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetDeniedTrips()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetPendingTrips()
        {
            throw new NotImplementedException();
        }

        public Trip GetTripById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrip(int id, Trip trip)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrip(int id)
        {
            throw new NotImplementedException();
        }

        public void AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }
    }
}
