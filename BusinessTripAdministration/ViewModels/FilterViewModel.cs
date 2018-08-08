using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BusinessTripAdministration.Commands;
using BusinessTripAdministration.Models;
using BusinessTripModels;
using BusinessTripModels.Models;
using Caliburn.Micro;

namespace BusinessTripAdministration.ViewModels
{
    class FilterViewModel : Screen
    {

        private List<string> departureLocationList;
        private List<string> statuses;
        private IRequest myRequest;
        public FilterViewModel(IRequest requestObject, List<string> depLoclist, List<string> stats)
        {
            myRequest = requestObject;
            departureLocationList = depLoclist;
            statuses = stats;

            departureLocationList.Insert(0, "Any Location");
            statuses.Insert(0, "Any Status");

            statusSelected = statuses[0];
            departureLocationSelected = departureLocationList[0];
            StartingAnyDate = true;
            EndingAnyDate = true;
            StartingSelectedDate = DateTime.Today;
            EndingSelectedDate = DateTime.Today;
            this.ShowCurrentWindow();
        }

        private string departureLocationSelected;
        private string statusSelected;
        private DateTime startingSelectedDate;
        private DateTime endingSelectedDate;
        private bool startingAnyDate;
        private bool exactStartingDate;
        private bool beforeStartingDate;
        private bool afterStartingDate;
        private bool endingAnyDate;
        private bool exactEndingDate;
        private bool beforeEndingDate;
        private bool afterEndingDate;

        public List<string> ComboBoxDepartureLocationCatalog
        {
            get { return departureLocationList; }
        }

        public List<string> ComboBoxStatusCatalog
        {
            get { return statuses; }
        }

        public string DepartureLocationSelected
        {
            get
            {
                return this.departureLocationSelected;
            }

            set
            {
                this.departureLocationSelected = value;
                this.NotifyOfPropertyChange(() => this.DepartureLocationSelected);
            }
        }


        public string StatusSelected
        {
            get
            {
                return this.statusSelected;
            }
            set
            {
                this.statusSelected = value;
                this.NotifyOfPropertyChange(() => this.StatusSelected);
            }
        }

        public DateTime StartingSelectedDate
        {
            get
            {
                return this.startingSelectedDate;
            }
            set
            {
                this.startingSelectedDate = value;
                this.NotifyOfPropertyChange(() => this.StartingSelectedDate);
            }
        }

        public DateTime EndingSelectedDate
        {
            get
            {
                return this.endingSelectedDate;
            }
            set
            {
                this.endingSelectedDate = value;
                this.NotifyOfPropertyChange(() => this.EndingSelectedDate);
            }
        }

        public bool StartingAnyDate
        {
            get
            {
                return this.startingAnyDate;
            }
            set
            {
                this.startingAnyDate = value;
                this.NotifyOfPropertyChange(() => this.StartingAnyDate);
            }
        }


        public bool ExactStartingDate
        {
            get
            {
                return this.exactStartingDate;
            }
            set
            {
                this.exactStartingDate = value;
                this.NotifyOfPropertyChange(() => this.ExactStartingDate);
            }
        }


        public bool BeforeStartingDate
        {
            get
            {
                return this.beforeStartingDate;
            }
            set
            {
                this.beforeStartingDate = value;
                this.NotifyOfPropertyChange(() => this.BeforeStartingDate);
            }
        }

        public bool AfterStartingDate
        {
            get
            {
                return this.afterStartingDate;
            }
            set
            {
                this.afterStartingDate = value;
                this.NotifyOfPropertyChange(() => this.AfterStartingDate);
            }
        }


        public bool EndingAnyDate
        {
            get
            {
                return this.endingAnyDate;
            }
            set
            {
                this.endingAnyDate = value;
                this.NotifyOfPropertyChange(() => this.EndingAnyDate);
            }
        }

        public bool ExactEndingDate
        {
            get
            {
                return this.exactEndingDate;
            }
            set
            {
                this.exactEndingDate = value;
                this.NotifyOfPropertyChange(() => this.ExactEndingDate);
            }
        }

        public bool BeforeEndingDate
        {
            get
            {
                return this.beforeEndingDate;
            }
            set
            {
                this.beforeEndingDate = value;
                this.NotifyOfPropertyChange(() => this.BeforeEndingDate);
            }
        }

        public bool AfterEndingDate
        {
            get
            {
                return this.afterEndingDate;
            }
            set
            {
                this.afterEndingDate = value;
                this.NotifyOfPropertyChange(() => this.AfterEndingDate);
            }
        }


        private string isCurrentWindowVisible;
        public string IsCurrentWindowVisible
        {
            get
            {
                return isCurrentWindowVisible;
            }
            set
            {
                isCurrentWindowVisible = value;
                NotifyOfPropertyChange(() => IsCurrentWindowVisible);
            }
        }



        private ICommand searchCommand;

        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new ButtonCommand(
                        param => this.Search(),
                        param => this.CanSearch()
                    );
                }
                return searchCommand;
            }
        }


        private void Search()
        {
            List<Trip> allTrips=new List<Trip>();
            if (StatusSelected=="Pending" || StatusSelected == "Any Status")
                allTrips.AddRange(RequestManager.PendingTripList);
            if (!(myRequest is RequestsViewModel))
            {
                if (StatusSelected == "Approved" || StatusSelected == "Any Status")
                    allTrips.AddRange(RequestManager.ApprovedTripList);
                if (StatusSelected == "Denied" || StatusSelected == "Any Status")
                    allTrips.AddRange(RequestManager.DeniedTripList);
            }
            if(departureLocationSelected != "Any Location")
                allTrips = RequestManager.SearchTripsByDepartureLocation(allTrips, departureLocationSelected).ToList();
            if (exactStartingDate == true)
                allTrips = RequestManager.SearchTripsByStartingDate(allTrips, StartingSelectedDate).ToList();
            else if (beforeStartingDate == true)
                allTrips = RequestManager.SearchTripsBeforeStartingDate(allTrips, StartingSelectedDate).ToList();
            else if (afterStartingDate == true)
                allTrips = RequestManager.SearchTripsAfterStartingDate(allTrips, StartingSelectedDate).ToList();

            if (exactEndingDate == true)
                allTrips = RequestManager.SearchTripsByEndingDate(allTrips, EndingSelectedDate).ToList();
            else if (beforeEndingDate == true)
                allTrips = RequestManager.SearchTripsBeforeEndingDate(allTrips, EndingSelectedDate).ToList();
            else if (afterEndingDate == true)
                allTrips = RequestManager.SearchTripsAfterEndingDate(allTrips, EndingSelectedDate).ToList();

            myRequest.ShowTrips(allTrips);
            HideCurrentWindow();
        }

        private bool CanSearch()
        {
            return true;
        }

        public void HideCurrentWindow()
        {
            IsCurrentWindowVisible = "Hidden";
        }
        public void ShowCurrentWindow()
        {
            IsCurrentWindowVisible = "Visible";
        }


    }
}
