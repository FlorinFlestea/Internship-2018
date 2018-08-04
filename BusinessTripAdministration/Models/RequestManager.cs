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

    }
}
