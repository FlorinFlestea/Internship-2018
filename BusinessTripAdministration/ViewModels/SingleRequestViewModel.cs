using System.Threading.Tasks;
using BusinessTripAdministration.Commands;
using Caliburn.Micro;
using System.Windows.Input;
using BusinessTripAdministration.Models;
using BusinessTripApplication.Server;
using BusinessTripModels.Models;
using System.Data.Entity;

namespace BusinessTripAdministration.ViewModels
{
    internal class SingleRequestViewModel 
    {
        private int id;
        private string user;
        private string destination;
        private string startDate;
        private string endDate;
        private RequestsViewModel parentRequestsViewModel;

        public SingleRequestViewModel(RequestsViewModel requestsViewModel, int id, string user, string destination, string startDate, string endDate)
        {
            parentRequestsViewModel = requestsViewModel;
            Id = id;
            User = user;
            Destination = destination;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;

            }
        }

        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
               
            }
        }

        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = value;
                
            }
        }
        public string StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
               
            }
        }
        public string EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }


        private ICommand acceptCommand;
        private ICommand denyCommand;
        private ICommand detailsCommand;

        public ICommand AcceptCommand
        {
            get
            {
                if (acceptCommand == null)
                {
                    acceptCommand = new ButtonCommand(
                        param => this.AcceptRequest(),
                        param => this.CanAccept()
                    );
                }
                return acceptCommand;
            }
        }



        public ICommand DenyCommand
        {
            get
            {
                if (denyCommand == null)
                {
                    denyCommand = new ButtonCommand(
                        param => this.DenyRequest(),
                        param => this.CanDeny()
                    );
                }
                return denyCommand;
            }
        }
        public ICommand DetailsCommand
        {
            get
            {
                if (detailsCommand == null)
                {
                    detailsCommand = new ButtonCommand(
                        param => this.DetailsRequest(),
                        param => this.CanRequestDetails()
                    );
                }
                return detailsCommand;
            }
        }

        private bool CanRequestDetails()
        {
            return true;
        }

        private async void DetailsRequest()
        {
            Trip trip = await RequestManager.GetTripById(id);
            var model = new DetailsViewModel(trip);
            IWindowManager manager = new WindowManager();
            manager.ShowWindow(model, context: null, settings: null);
        }

        public async void AcceptRequest()
        {
            await RequestManager.ApproveTrip(Id);
            parentRequestsViewModel.RefreshUnapporvedRequests();

            SendEmail(true);
        }
        private bool CanAccept()
        {
            return true;
        }
        public async void DenyRequest()
        {
            await RequestManager.DenyTrip(Id);
            parentRequestsViewModel.RefreshUnapporvedRequests();
            SendEmail(false);
        }
        private bool CanDeny()
        {
            return true;
        }

        private async void SendEmail(bool acceptOrDeny)
        {
            EmailSender emailSender = new EmailSender();
            string message;
            if (acceptOrDeny)
                message = "We are excited to tell you that your trip with id "
                             + id + " has been accepted.";
            else message = "We are sorry to tell you that your trip with id "
                          + id + " has been denied.";
            Trip trip = await RequestManager.GetTripById(id);
            User user = trip.User;
            if (user != null)
            {
                emailSender.SendEmail(user.Email, "Trip Request", message);
            }
        }

    }
}
