using BusinessTripAdministration.Models;
using BusinessTripModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessTripAdministration.Commands;

namespace BusinessTripAdministration.ViewModels
{
    class HistoryViewModel: Conductor<object>
    {
        
        public HistoryViewModel()
        {
            requestList = new List<SingleHistoryViewModel>();
            RefreshAllRequests();
        }
        private List<SingleHistoryViewModel> requestList;
        public List<SingleHistoryViewModel> RequestList
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

        private ICommand refreshCommand;

        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new ButtonCommand(
                        param => this.RefreshAllRequests(),
                        param => this.CanRefresh()
                    );
                }
                return refreshCommand;
            }
        }

        private bool CanRefresh()
        {
            return true;
        }


        private async void RefreshAllRequests()
        {
            await RequestManager.RefreshApprovedRequestsFromDatabase();
            await RequestManager.RefreshDeniedRequestsFromDatabase();
            ShowTrips();
        }

        private void ShowTrips()
        {
            List<Trip> tripList = RequestManager.DeniedTripList;
            tripList.AddRange(RequestManager.ApprovedTripList);
            List <SingleHistoryViewModel> list = new List<SingleHistoryViewModel>();
            foreach (Trip trip in tripList)
            {
                list.Add(new SingleHistoryViewModel(trip.Id,trip.ClientName, trip.DepartureLocation, trip.StartingDate.Value.ToString("dd/MM/yyyy"), trip.EndDate.Value.ToString("dd/MM/yyyy")));
            }
            RequestList = list;
        }

    }
}
