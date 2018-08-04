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
            var tripList = trips.ToList();
            foreach(Trip trip in tripList)
            {
                 list.Add(new SingleRequestViewModel(trip.ClientName,trip.DepartureLocation,trip.StartingDate.Value.ToString("dd/MM/yyyy"),trip.EndDate.Value.ToString("dd/MM/yyyy")));
            }

            RequestList = list;
        }

    }
}
