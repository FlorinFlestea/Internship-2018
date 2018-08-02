using BusinessTripAdministration.Models;
using BusinessTripModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private void GetAllUnapporvedRequestsFromDatabase()
        {
            List<Trip> tripList = ApiClient.GetPendingTrips().Result.ToList();
            if (tripList.Count == 0)
                return;
            foreach(Trip trip in tripList)
            {
                RequestList.Add(new SingleRequestViewModel(trip.ClientName,trip.DepartureLocation.ToString(),trip.StartingDate.ToString(),trip.EndDate.ToString()));
            }
        }

    }
}
