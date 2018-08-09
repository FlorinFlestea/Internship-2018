using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BusinessTripModels.Models;
using BussinesTripModels.Models;
using Newtonsoft.Json;

namespace BusinessTripAdministration.Models
{
    public class ApiClient : IApiClient
    {
        static readonly HttpClient client = new HttpClient();

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
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var returnList = JsonConvert.DeserializeObject<IEnumerable<Trip>>(jsonResponse);

            return returnList;
        }

        // GET: /api/TripsApi/approved
        public async Task<IEnumerable<Trip>> GetApprovedTrips()
        {
            var response = await client.GetAsync("/api/TripsApi/approved");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var returnList = JsonConvert.DeserializeObject<IEnumerable<Trip>>(jsonResponse);

            return returnList;
        }

        // GET: /api/TripsApi/denied
        public async Task<IEnumerable<Trip>> GetDeniedTrips()
        {
            var response = await client.GetAsync("/api/TripsApi/denied");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var returnList = JsonConvert.DeserializeObject<IEnumerable<Trip>>(jsonResponse);

            return returnList;
        }

        // GET: /api/TripsApi/pending
        public async Task<IEnumerable<Trip>> GetPendingTrips()
        {
            var response = await client.GetAsync("/api/TripsApi/pending");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var returnList = JsonConvert.DeserializeObject<IEnumerable<Trip>>(jsonResponse);


            return returnList;
        }

        // GET: /api/TripsApi/id
        public async Task<Trip> GetTripById(int id)
        {
            var response = await client.GetAsync("/api/TripsApi/" + id);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var returnObject = JsonConvert.DeserializeObject<Trip>(jsonResponse);

            return returnObject;
        }

        // PUT: /api/TripsApi
        public async Task<bool> UpdateTrip(int tripId, Trip newTrip)
        {
            var jsonString = JsonConvert.SerializeObject(new UpdateTripModel(tripId, newTrip));
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/api/TripsApi/",httpContent);
            var returnResponse = response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        // POST: /api/TripsApi
        public async Task<bool> AddTrip(Trip trip)
        {
            var jsonString = JsonConvert.SerializeObject(trip);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/TripsApi/", httpContent);

            return response.IsSuccessStatusCode;
        }

        // DELETE: api/TripsApi/id
        public async Task<bool> DeleteTrip(int id)
        {
            var response = await client.DeleteAsync("/api/TripsApi/" + id);

            return response.IsSuccessStatusCode;
        }
    }
}
