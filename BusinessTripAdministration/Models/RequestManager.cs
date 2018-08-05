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
        public static List<Trip> PendingTripList = new List<Trip>();
        public static List<Trip> DeniedTripList = new List<Trip>();
        public static List<Trip> ApprovedTripList = new List<Trip>();
        //does nothing, but it is used to load class into memory
        //to speed up loading data
        //should be called only once
        public static void Init()
        {
            RefreshPendingRequestsFromDatabase();
            RefreshApprovedRequestsFromDatabase();
            RefreshDeniedRequestsFromDatabase();
        }

        public static async Task RefreshApprovedRequestsFromDatabase()
        {
            var trips = await ApiClient.GetApprovedTrips();
            ApprovedTripList = trips.ToList();
        }

        public static async Task RefreshDeniedRequestsFromDatabase()
        {
            var trips = await ApiClient.GetDeniedTrips();
            DeniedTripList = trips.ToList();
        }

        public static async Task RefreshPendingRequestsFromDatabase()
        {
            var trips = await ApiClient.GetPendingTrips();
            PendingTripList = trips.ToList();
        }

        public static async void ApproveTrip(int id)
        {
            Trip trip = PendingTripList.Find(t => t.Id == id);
            trip.Status = 1;//1 means approved
            await ApiClient.UpdateTrip(id, trip);
        }

        public static async void DenyTrip(int id)
        {
            Trip trip = PendingTripList.Find(t => t.Id == id);
            trip.Status = 0;//0 means denied
            await ApiClient.UpdateTrip(id, trip);
        }

        public static async void AddTrip(Trip trip)
        {
            await ApiClient.AddTrip(trip);
        }

        public static async void DeleteTrip(Trip trip)
        {
            await ApiClient.DeleteTrip(trip.Id);
        }

        public static async Task<Trip> GetTripById(int id)
        {
            Trip trip = await ApiClient.GetTripById(id);
            return trip;
        }

        public static IEnumerable<Trip> SearchTripsByLocation(List<Trip> tripList, string location)
        {
            return tripList.Where(trip => trip.ClientLocation == location);
        }

        public static IEnumerable<Trip> SearchTripsByStatus(List<Trip> tripList, int status)
        {
            return tripList.Where(trip => trip.Status == status);
        }

        public static IEnumerable<Trip> SearchTripsByStartingDate(List<Trip> tripList, DateTime startingDate)
        {
            return tripList.Where(trip => trip.StartingDate.Value.Day == startingDate.Day);
        }

        public static IEnumerable<Trip> SearchTripsByEndingDate(List<Trip> tripList, DateTime endingDate)
        {
            return tripList.Where(trip => trip.StartingDate.Value.Day == endingDate.Day);
        }


    }
}
