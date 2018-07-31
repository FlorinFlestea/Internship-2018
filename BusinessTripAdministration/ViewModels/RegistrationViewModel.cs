using BusinessTripAdministration.Commands;
using BusinessTripAdministration.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessTripAdministration.ViewModels
{
    internal class RegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string Username { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
        private string ConfirmPassword { get; set; }
        private string[] messages;

        public RegistrationViewModel()
        {
            messages = new string[3];
        }

        private ICommand registerCommand;
        /*
      * protected void OnPropertyChanged(string name)
      {
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
      }
      */

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

  
        private void Register()
        {

        }
        private bool CanRegister()
        { 

            if( ValidateField.Email(Email,ref messages[0])
                &&ValidateField.Username(Username,ref messages[1])
                && ValidateField.Password(Password, ref messages[2])
               )
            {
                return true;
            }
            return false;
        }
    }
}
