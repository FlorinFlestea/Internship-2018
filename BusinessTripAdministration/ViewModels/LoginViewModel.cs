using BusinessTripAdministration.Commands;
using BusinessTripAdministration.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessTripAdministration.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private string Email { get; set; }
        private string Password { get; set; }
        private bool RememberMe { get; set; }

        public LoginViewModel() { }


        /*
         * protected void OnPropertyChanged(string name)
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
         }
         */

        private ICommand loginCommand;
        private ICommand registerCommand;

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
        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                {
                    registerCommand = new ButtonCommand(
                        param => this.Register(),
                        param => this.CanRegister()
                    );
                }
                return registerCommand;
            }
        }

        private void Login()
        {

        }
        private bool CanLogin()
        {
            bool canLogin = false;
            return canLogin;
        }
        private void Register()
        {

        }
        private bool CanRegister()
        {
            return true;
        }

    }
}
