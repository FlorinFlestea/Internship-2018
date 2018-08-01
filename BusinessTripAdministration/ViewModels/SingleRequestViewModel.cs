using BusinessTripAdministration.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTripAdministration.ViewModels
{
    public class SingleRequestViewModel : Screen
    {
        public string User{get;set;}
        public string Destination { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public SingleRequestViewModel(string user, string destination,string startDate,string endDate)
        {
            User = user;
            Destination = destination;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void AcceptRequest()
        {

        }
        public void DenyRequest()
        {

        }

    }
}
