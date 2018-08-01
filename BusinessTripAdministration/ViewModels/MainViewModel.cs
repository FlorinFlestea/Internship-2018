using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusinessTripAdministration.ViewModels
{
    internal class MainViewModel : Conductor<object>,IViewModel
    {
        public MainViewModel()
        {
            LoadHome();
        }

        public void LoadHome()
        {
            ActivateItem(new HomeViewModel());
        }
        public void LoadRequests()
        {
            ActivateItem(new RequestsViewModel());
        }
        
    }
}
