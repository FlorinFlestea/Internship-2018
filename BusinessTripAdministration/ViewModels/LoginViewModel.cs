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
    internal class LoginViewModel : Conductor<object>
    {

        private string email;
        public string Email
        {
            get
            { return email; }
            set
            {   email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public LoginViewModel() { }

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
            //ActivateItem();
        }

    }
}
