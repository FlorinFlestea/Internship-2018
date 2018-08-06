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

        public MainViewModel()
        {
            ShowCurrentWindow();
            LoadHome();
        }

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

        
        

        public void Logout()
        {
            LoginViewModel.RemoveUserCredentials();
            HideCurrentWindow();
            LoginViewModel.MainLoginViewModelInstance.ShowCurrentWindow();
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


        void HideCurrentWindow()
        {
            IsCurrentWindowVisible = "Hidden";
        }
        void ShowCurrentWindow()
        {
            IsCurrentWindowVisible = "Visible";
        }

    }
}
