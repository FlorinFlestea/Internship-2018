using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessTripAdministration.ViewModels;
using BusinessTripModels.Models;

namespace BusinessTripAdministration.Models
{
    public static class RequestManager
    {
        private static readonly ApiClient ApiClient = new ApiClient();
        public static List<Trip> PendingTripList = new List<Trip>();
        public static List<Trip> DeniedTripList = new List<Trip>();
        public static List<Trip> ApprovedTripList = new List<Trip>();

        private static List<string> departureLocationList;

        public static List<string> DepartureLocationList
        {
            get
            {
                departureLocationList = GetAllDeparturedLocations();
                return departureLocationList;
            }
        }

        //does nothing, but it is used to load class into memory
        //to speed up loading data
        //should be called only once
         public static void Init()
        {
            RefreshPendingRequestsFromDatabase();
            RefreshApprovedRequestsFromDatabase();
            RefreshDeniedRequestsFromDatabase();
        }

        private static List<string> GetAllDeparturedLocations()
        {
            List<string> locationList=new List<string>();
            //IMPORTANT
            //we do not want to modify the original list
            List<Trip> allTrips = new List<Trip>();
            allTrips.AddRange(PendingTripList);
            //IMPORTANT
            allTrips.AddRange(DeniedTripList);
            allTrips.AddRange(ApprovedTripList);
            foreach (Trip trip in allTrips)
                if (!locationList.Contains(trip.DepartureLocation))
                    locationList.Add(trip.DepartureLocation);
            return locationList;
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

        public static async Task ApproveTrip(int id)
        {
            Trip trip = PendingTripList.Find(t => t.Id == id);
            trip.Status = 1;//1 means approved
            await ApiClient.UpdateTrip(id, trip);
        }

        public static async Task DenyTrip(int id)
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

        public static IEnumerable<Trip> SearchTripsByDepartureLocation(List<Trip> tripList, string location)
        {
            return tripList.Where(trip => trip.DepartureLocation == location);
        }


        public static IEnumerable<Trip> SearchTripsByStartingDate(List<Trip> tripList, DateTime startingDate)
        {
            return tripList.Where(trip => trip.StartingDate.Value == startingDate);
        }

        public static IEnumerable<Trip> SearchTripsBeforeStartingDate(List<Trip> tripList, DateTime startingDate)
        {
            return tripList.Where(trip => trip.StartingDate.Value <= startingDate);
        }

        public static IEnumerable<Trip> SearchTripsAfterStartingDate(List<Trip> tripList, DateTime startingDate)
        {
            return tripList.Where(trip => trip.StartingDate.Value >= startingDate);
        }

        public static IEnumerable<Trip> SearchTripsByEndingDate(List<Trip> tripList, DateTime endingDate)
        {
            return tripList.Where(trip => trip.EndDate.Value == endingDate);
        }

        public static IEnumerable<Trip> SearchTripsBeforeEndingDate(List<Trip> tripList, DateTime endingDate)
        {
            return tripList.Where(trip => trip.EndDate.Value <= endingDate);
        }

        public static IEnumerable<Trip> SearchTripsAfterEndingDate(List<Trip> tripList, DateTime endingDate)
        {
            return tripList.Where(trip => trip.EndDate.Value >= endingDate);
        }


    }
}
