using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTripAdministration.Models
{
    public class ApiClient : IApiClient
    {
        static HttpClient client = new HttpClient();

        public ApiClient()
        {
            client.BaseAddress = new Uri("https://localhost:44328");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: /api/TripsApi
        public async Task<IEnumerable<Trip>> GetAllTrips()
        {
            var response = await client.GetAsync("/api/tripsapi");

            throw new NotImplementedException();
        }

        // GET: /api/TripsApi/approved
        public async Task<IEnumerable<Trip>> GetApprovedTrips()
        {
            throw new NotImplementedException();
        }

        // GET: /api/TripsApi/denied
        public async Task<IEnumerable<Trip>> GetDeniedTrips()
        {
            throw new NotImplementedException();
        }

        // GET: /api/TripsApi/pending
        public async Task<IEnumerable<Trip>> GetPendingTrips()
        {
            throw new NotImplementedException();
        }

        // GET: /api/TripsApi/id
        public async Task<Trip> GetTripById(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: /api/TripsApi
        public async void UpdateTrip(int tripId, Trip newTrip)
        {
            throw new NotImplementedException();
        }

        // POST: /api/TripsApi
        public async void AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/TripsApi/id
        public async void DeleteTrip(int id)
        {
            throw new NotImplementedException();
        }
    }
}
