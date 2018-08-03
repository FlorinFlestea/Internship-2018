using BusinessTripAdministration.Models;
using BusinessTripModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessTripAdministration.ViewModels
{
    class RequestsViewModel: Conductor<object>
    {
        private ApiClient ApiClient;
        public RequestsViewModel(ApiClient apiClient)
        {
            requestList = new List<SingleRequestViewModel>();
            ApiClient = apiClient;
            GetAllUnapporvedRequestsFromDatabase();
        }
        private List<SingleRequestViewModel> requestList;
        public List<SingleRequestViewModel> RequestList
        {
            get
            {
                return requestList;
            }
            set
            {
                requestList = value;
                NotifyOfPropertyChange(() => RequestList);
            }
        }

        private async void GetAllUnapporvedRequestsFromDatabase()
        {
            List<SingleRequestViewModel> list = new List<SingleRequestViewModel>();
            var trips = await ApiClient.GetPendingTrips();
            //tripList.Wait();
            var tripList = trips.ToList();
            //var tripList = trips.Result.ToList();
            Console.WriteLine(tripList);


            
            foreach(Trip trip in tripList)
            {
                 list.Add(new SingleRequestViewModel(trip.ClientName,trip.DepartureLocation.ToString(),trip.StartingDate.ToString(),trip.EndDate.ToString()));
            }

            RequestList = list;
            Thread.Sleep(3000);
        }

    }
}
