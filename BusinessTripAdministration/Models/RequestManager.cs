using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessTripAdministration.ViewModels;
using BusinessTripModels;

namespace BusinessTripAdministration.Models
{
    public static class RequestManager
    {
        private static readonly ApiClient ApiClient = new ApiClient();
        public static List<Trip> TripList = new List<Trip>();

        //does nothing, but it is used to load class into memory
        //to speed up loading data
        //should be called only once
        public static void Init()
        {
            RefreshUnapporvedRequestsFromDatabase();
        }

        public static async Task RefreshUnapporvedRequestsFromDatabase()
        {
            var trips = await ApiClient.GetPendingTrips();
            TripList = trips.ToList();
        }

        public static async void ApproveTrip(int id)
        {
            Trip trip = TripList.Find(t => t.Id == id);
            trip.Status = 1;//1 means approved
            await ApiClient.UpdateTrip(id, trip);
        }

        public static async void DenyTrip(int id)
        {
            Trip trip = TripList.Find(t => t.Id == id);
            trip.Status = 0;//0 means denied
            await ApiClient.UpdateTrip(id, trip);
        }

        public static async void AddTrip(Trip trip)
        {
            Trip trip2 = TripList[0];
            trip2.ClientName = "Goodffffff";
            await ApiClient.AddTrip(trip2);
        }

        public static async void DeleteTrip(Trip trip)
        {
            Trip trip2 = TripList[0];
            await ApiClient.DeleteTrip(trip2.Id);
        }

        public static async Task<Trip> GetTripById(int id)
        {
            Trip trip = await ApiClient.GetTripById(id);
            return trip;
        }

    }
}
