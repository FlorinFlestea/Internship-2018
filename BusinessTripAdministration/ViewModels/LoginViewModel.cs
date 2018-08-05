using BusinessTripAdministration.Commands;
using BusinessTripAdministration.Models;
using BusinessTripAdministration.Validation;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;

namespace BusinessTripAdministration.ViewModels
{
    internal class LoginViewModel: Screen
    {
        public string Email { get; set; }
        public string Password {private get; set; }
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
            RequestManager.Init();
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
            if (DatabaseQuery.Login(Email, Password) == true)
                LoadMainPage();
            else
                MessageBox.Show("Invalid Username or Password");
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
            MainViewModel main = new MainViewModel();
            manager.ShowWindow(main, context: null, settings: null);
            main.Email = Email;
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
