using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BusinessTripApplication.Models;
using Newtonsoft.Json;

namespace BusinessTripAdministration.Models
{
    public class ApiClient : IApiClient
    {
        private const string RequestLink = "https://localhost:44328/api/tripsapi/";
        private static readonly HttpClient client = new HttpClient();

        public ApiClient()
        {
            client.BaseAddress = new Uri(RequestLink);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }



        // GET : /api/tripsapi/
        async Task<IEnumerable<Trip>> IApiClient.GetAllTrips()
        {
            var response = client.GetAsync("");

            if (response.Result.IsSuccessStatusCode)
            {
                var jsonString = await response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<Trip>>(jsonString);
                return result;
            }

            throw new NotImplementedException();
        }

        // GET : /api/tripsapi/approved
        Task<IEnumerable<Trip>> IApiClient.GetApprovedTrips()
        {
            throw new NotImplementedException();
        }

        // GET : /api/tripsapi/denied
        Task<IEnumerable<Trip>> IApiClient.GetDeniedTrips()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Trip>> IApiClient.GetPendingTrips()
        {
            throw new NotImplementedException();
        }

        Task<Trip> IApiClient.GetTripById(int id)
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
