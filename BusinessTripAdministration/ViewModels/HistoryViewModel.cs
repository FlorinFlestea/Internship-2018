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
    class HistoryViewModel: Conductor<object>, IRequest
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


        private ICommand filterCommand;

        public ICommand FilterCommand
        {
            get
            {
                if (filterCommand == null)
                {
                    filterCommand = new ButtonCommand(
                        param => this.LoadFilterPage(),
                        param => this.CanFilter()
                    );
                }
                return filterCommand;
            }
        }

        private bool CanFilter()
        {
            return true;
        }

        void LoadFilterPage()
        {
            IWindowManager manager = new WindowManager();
            List<String> statuses = new List<string>()
            {
                "Pending",
                "Approved",
                "Denied"
            };
            FilterViewModel model = new FilterViewModel(this, RequestManager.DepartureLocationList, statuses);
            manager.ShowWindow(model, context: null, settings: null);
        }


        private async void RefreshAllRequests()
        {
            await RequestManager.RefreshApprovedRequestsFromDatabase();
            await RequestManager.RefreshDeniedRequestsFromDatabase();
            //IMMPORTANT
            //MUST create new list, else the original lists will be modified
            List<Trip> tripList =new List<Trip>();
            tripList.AddRange(RequestManager.ApprovedTripList);
            tripList.AddRange(RequestManager.DeniedTripList);
            //IMMPORTANT
            ShowTrips(tripList);
        }

        public void ShowTrips(List<Trip> tripList)
        {
            List <SingleHistoryViewModel> list = new List<SingleHistoryViewModel>();
            foreach (Trip trip in tripList)
            {
                list.Add(new SingleHistoryViewModel(trip.Id,trip.ClientName, trip.DepartureLocation, trip.StartingDate.Value.ToString("dd/MM/yyyy"), trip.EndDate.Value.ToString("dd/MM/yyyy")));
            }
            RequestList = list;
        }

    }
}
