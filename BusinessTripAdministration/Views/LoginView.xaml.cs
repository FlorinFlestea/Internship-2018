using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BusinessTripAdministration.Models;
namespace BusinessTripAdministration.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            Thread.Sleep(10000);

            ApiClient api = new ApiClient();

            var trips =  api.GetAllTrips();

            Console.WriteLine(trips);

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }
    }
}
