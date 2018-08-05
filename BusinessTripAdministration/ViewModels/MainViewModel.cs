using Caliburn.Micro;
using BusinessTripAdministration.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BusinessTripAdministration.ViewModels
{
    internal class MainViewModel : Conductor<object>
    {
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }
        public MainViewModel()
        {
            LoadHome();
        }

        public void LoadHome()
        {
            ActivateItem(new HomeViewModel());
        }


        public void LoadRequests()
        {
            ActivateItem(new RequestsViewModel());
        }

        public void LoadHistory()
        {
            ActivateItem(new HistoryViewModel());
        }

    }
}
