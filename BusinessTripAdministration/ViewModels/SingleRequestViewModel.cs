using BusinessTripAdministration.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusinessTripAdministration.ViewModels
{
    public class SingleRequestViewModel : Screen
    {
        private string user;
        private string destination;
        private string startDate;
        private string endDate;
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                NotifyOfPropertyChange(() => User);
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
                NotifyOfPropertyChange(() => Destination);
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
                NotifyOfPropertyChange(() => StartDate);
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
                NotifyOfPropertyChange(() => EndDate);
            }
        }

        public SingleRequestViewModel(string user, string destination,string startDate,string endDate)
        {
            User = user;
            Destination = destination;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void AcceptRequest()
        {
            MessageBox.Show("Binding Works");
        }
        public void DenyRequest()
        {

        }

    }
}
