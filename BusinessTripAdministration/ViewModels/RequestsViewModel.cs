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
        private ApiClient apiClient;
        public RequestsViewModel()
        {
            requestList = new List<SingleRequestViewModel>();
            apiClient = new ApiClient();
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
            List<Trip> tripList = apiClient.GetPendingTrips().Result.ToList();
            foreach(Trip trip in tripList)
            {
                RequestList.Add(new SingleRequestViewModel(trip.ClientName,trip.DepartureLocation.ToString(),trip.StartingDate.ToString(),trip.EndDate.ToString()));
            }
        }

    }
}
