using BusinessTripAdministration.Commands;
using BusinessTripAdministration.Validation;
using BusinessTripAdministration.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BusinessTripAdministration.ViewModels
{
    internal class LoginViewModel: Screen
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
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

        public LoginViewModel()
        {

            ShowCurrentWindow();
        }

        private ICommand loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new ButtonCommand(
                        param => this.Login(),
                        param => this.CanLogin()
                    );
                }
                return loginCommand;
            }
        }

        private void Login()
        {
            LoadMainPage();
        }
        private bool CanLogin()
        {
           
            string emailErrors = "";
            string passwordErrors = "";
            bool canLogin = false;
            if ((ValidateField.Email(Email, ref emailErrors) && ValidateField.Password(Password, ref passwordErrors)) == true)
            {
                canLogin = true;
            }
            return canLogin;
        }

        void LoadMainPage()
        {
            HideCurrentWindow();
            IWindowManager manager = new WindowManager();
            manager.ShowWindow(new MainViewModel(), null, null);
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
