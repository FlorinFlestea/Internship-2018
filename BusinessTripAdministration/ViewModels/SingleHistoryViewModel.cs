using BusinessTripAdministration.Commands;
using Caliburn.Micro;
using System.Windows.Input;
using BusinessTripAdministration.Models;
using BusinessTripModels.Models;

namespace BusinessTripAdministration.ViewModels
{
    internal class SingleHistoryViewModel 
    {
        private int id;
        private string user;
        private string destination;
        private string startDate;
        private string endDate;

        public SingleHistoryViewModel(int id, string user, string destination, string startDate, string endDate)
        {
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


        
        private ICommand detailsCommand;

        
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
    }
}
