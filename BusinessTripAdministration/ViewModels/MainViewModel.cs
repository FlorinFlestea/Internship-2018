using BusinessTripAdministration.Models;
using Caliburn.Micro;
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
        private ApiClient apiClient;
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
            //Thread.Sleep(12000);
            apiClient = new ApiClient();
            LoadHome();
        }

        public void LoadHome()
        {
            ActivateItem(new HomeViewModel());
        }
        public void LoadRequests()
        {
            ActivateItem(new RequestsViewModel(apiClient));
        }
        
    }
}
