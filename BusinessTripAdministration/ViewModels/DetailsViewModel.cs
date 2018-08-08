using System;
using System.Windows.Input;
using BusinessTripAdministration.Commands;
using BusinessTripModels.Models;
using Caliburn.Micro;

namespace BusinessTripAdministration.ViewModels
{
    class DetailsViewModel : Screen
    {
        private Trip trip;
        

        public DetailsViewModel(Trip trip)
        {
            this.trip = trip;
            InitialiseFields();
        }

        private void InitialiseFields()
        {
            id = trip.Id;
            pmName = trip.PmName;
            clientName = trip.ClientName;
            startingDate = trip.StartingDate;
            endDate = trip.EndDate;
            projectName = trip.ProjectName;
            projectNumber = trip.ProjectNumber;
            taskName = trip.TaskName;
            taskNumber = trip.TaskNumber;
            clientLocation = trip.ClientLocation;
            departureLocation = trip.DepartureLocation;
            transportation = trip.Transportation;
            needOfPhone = trip.NeedOfPhone;
            needOfBankCard = trip.NeedOfBankCard;
            accommodation = trip.Accommodation;
            comments = trip.Comments;
            switch (trip.Status)
            {
                case 0: status = "Denied"; break;
                case 1: status = "Approved"; break;
                default: status = "Pending"; break;
            }
            if(trip.User!=null)
                user = trip.User.Email;
            if (trip.Area != null)
                area = trip.Area.Name;
        }

        private int id;
        
        private string pmName ;

        private string clientName ;

        private Nullable<DateTime> startingDate ;

        private Nullable<DateTime> endDate ;

        private string projectName ;

        private string projectNumber ;

        private string taskName ;

        private string taskNumber ;

        private string clientLocation ;

        private string departureLocation ;

        private string transportation ;

        private bool needOfPhone ;

        private bool needOfBankCard ;

        private string accommodation ;

        private string comments ;

        private string status ;

        private string user ;
        private string area ;








        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                this.NotifyOfPropertyChange(() => this.Id);
            }
        }


        public string PmName
        {
            get
            {
                return this.pmName;
            }
            set
            {
                this.pmName = value;
                this.NotifyOfPropertyChange(() => this.PmName);
            }
        }


        public string ClientName
        {
            get
            {
                return this.clientName;
            }
            set
            {
                this.clientName = value;
                this.NotifyOfPropertyChange(() => this.ClientName);
            }
        }

        public Nullable<DateTime> StartingDate
        {
            get
            {
                return this.startingDate;
            }
            set
            {
                this.startingDate = value;
                this.NotifyOfPropertyChange(() => this.StartingDate);
            }
        }

        public Nullable<DateTime> EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
                this.NotifyOfPropertyChange(() => this.EndDate);
            }
        }

        public string ProjectName
        {
            get
            {
                return this.projectName;
            }
            set
            {
                this.projectName = value;
                this.NotifyOfPropertyChange(() => this.ProjectName);
            }
        }

        public string ProjectNumber
        {
            get
            {
                return this.projectNumber;
            }
            set
            {
                this.projectNumber = value;
                this.NotifyOfPropertyChange(() => this.ProjectNumber);
            }
        }

        public string TaskName
        {
            get
            {
                return this.taskName;
            }
            set
            {
                this.taskName = value;
                this.NotifyOfPropertyChange(() => this.TaskName);
            }
        }

        public string TaskNumber
        {
            get
            {
                return this.taskNumber;
            }
            set
            {
                this.taskNumber = value;
                this.NotifyOfPropertyChange(() => this.TaskNumber);
            }
        }


        public string ClientLocation
        {
            get
            {
                return this.clientLocation;
            }
            set
            {
                this.clientLocation = value;
                this.NotifyOfPropertyChange(() => this.ClientLocation);
            }
        }

        public string DepartureLocation
        {
            get
            {
                return this.departureLocation;
            }
            set
            {
                this.departureLocation = value;
                this.NotifyOfPropertyChange(() => this.DepartureLocation);
            }
        }

        public string Transportation
        {
            get
            {
                return this.transportation;
            }
            set
            {
                this.transportation = value;
                this.NotifyOfPropertyChange(() => this.Transportation);
            }
        }

        public bool NeedOfPhone
        {
            get
            {
                return this.needOfPhone;
            }
            set
            {
                this.needOfPhone = value;
                this.NotifyOfPropertyChange(() => this.NeedOfPhone);
            }
        }

        public bool NeedOfBankCard
        {
            get
            {
                return this.needOfBankCard;
            }
            set
            {
                this.needOfBankCard = value;
                this.NotifyOfPropertyChange(() => this.NeedOfBankCard);
            }
        }

        public string Accommodation
        {
            get
            {
                return this.accommodation;
            }
            set
            {
                this.accommodation = value;
                this.NotifyOfPropertyChange(() => this.Accommodation);
            }
        }

        public string Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
                this.NotifyOfPropertyChange(() => this.Comments);
            }
        }
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                this.NotifyOfPropertyChange(() => this.Status);
            }
        }

        public string User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
                this.NotifyOfPropertyChange(() => this.User);
            }
        }

        public string Area
        {
            get
            {
                return this.area;
            }
            set
            {
                this.area = value;
                this.NotifyOfPropertyChange(() => this.Area);
            }
        }

        private ICommand backCommand;

        public ICommand BackCommand
        {
            get
            {
                if (backCommand == null)
                {
                    backCommand = new ButtonCommand(
                        param => this.GoBack(),
                        param => this.CanGoBack()
                    );
                }
                return backCommand;
            }
        }


        private void GoBack()
        {
            this.TryClose();
        }

        private bool CanGoBack()
        {
            return true;
        }

    }
}
