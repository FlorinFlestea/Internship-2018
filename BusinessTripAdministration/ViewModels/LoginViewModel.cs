using System;
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
        public static LoginViewModel MainLoginViewModelInstance { get; set; }
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
            if (MainLoginViewModelInstance == null)
            {
                MainLoginViewModelInstance = this;
            }
            else throw new Exception("Cannot create multiple Main Windows");
            ShowCurrentWindow();
            //Very Important
            //need to call it exactly one time
            RequestManager.Init();
            //check if remember me option was set last time
            CheckIfAutomaticLogin();
        }

        private void CheckIfAutomaticLogin()
        {
            var email = Properties.Settings.Default.Email;
            var password = Properties.Settings.Default.Password;
            if (email != "" && password != "")
            {
                if (DatabaseQuery.Login(email, password) == true)
                {
                    Password = password;
                    Email = email;
                    LoadMainPage();
                }
                    
            }
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

        private void SaveUserCredentials()
        {
            Properties.Settings.Default.Email = Email;
            Properties.Settings.Default.Password = Password;
            Properties.Settings.Default.Save();
        }

        public static void RemoveUserCredentials()
        {
            Properties.Settings.Default.Email = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();
        }

        private void Login()
        {
            if (DatabaseQuery.Login(Email, Password) == true)
            {
                if (RememberMe == true)
                    SaveUserCredentials();
                else
                    RemoveUserCredentials();

                LoadMainPage();
            }
                
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

        public void HideCurrentWindow()
        {
            IsCurrentWindowVisible = "Hidden";
        }

        public void ShowCurrentWindow()
        {
            IsCurrentWindowVisible = "Visible";
        }

    }
}
