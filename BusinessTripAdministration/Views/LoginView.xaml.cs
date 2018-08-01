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
            DatabaseQuery.Login("cernovalex1@gmail.com", "Test123!");

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
