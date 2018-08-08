using System;
using BusinessTripAdministration.Models;
using BusinessTripModels.Models;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Windows.Input;
using BusinessTripAdministration.Commands;

namespace BusinessTripAdministration.ViewModels
{
    class RequestsViewModel: Conductor<object>, IRequest
    {
        private FilterViewModel MyFilterViewModel;
        public RequestsViewModel()
        {
            requestList = new List<SingleRequestViewModel>();
            RefreshUnapporvedRequests();
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

        private ICommand refreshCommand;

        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new ButtonCommand(
                        param => this.RefreshUnapporvedRequests(),
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
            if (MyFilterViewModel == null || MyFilterViewModel.IsActive == false)
                InitialiseMyFilter();
            MyFilterViewModel.ShowCurrentWindow();
        }


        private ICommand approveAllCommand;
        public ICommand ApproveAllCommand
        {
            get
            {
                if (approveAllCommand == null)
                {
                    approveAllCommand = new ButtonCommand(
                        param => this.ApproveAll(),
                        param => this.CanApproveAll()
                    );
                }
                return approveAllCommand;
            }
        }
        private bool CanApproveAll()
        {
            return true;
        }

        public async void ApproveAll()
        {
            List<Trip> pendingTrips = RequestManager.PendingTripList;
            foreach (Trip trip in pendingTrips)
            {
                await RequestManager.ApproveTrip(trip.Id);
            }
            RefreshUnapporvedRequests();
        }

        private void InitialiseMyFilter()
        {
            List<String> statuses = new List<string>();
            MyFilterViewModel = new FilterViewModel(this, RequestManager.DepartureLocationList, statuses);
            IWindowManager manager = new WindowManager();
            manager.ShowWindow(MyFilterViewModel, context: null, settings: null);
            MyFilterViewModel.HideCurrentWindow();
        }

        public async void RefreshUnapporvedRequests()
        {
            await RequestManager.RefreshPendingRequestsFromDatabase();
            ShowTrips(RequestManager.PendingTripList);
        }

        public void ShowTrips(List<Trip> tripList)
        {
            List <SingleRequestViewModel> list = new List<SingleRequestViewModel>();
            foreach (Trip trip in tripList)
            {
                list.Add(new SingleRequestViewModel(this,trip.Id,trip.ClientName, trip.DepartureLocation, trip.StartingDate.Value.ToString("dd/MM/yyyy"), trip.EndDate.Value.ToString("dd/MM/yyyy")));
            }
            RequestList = list;
        }

    }
}
