using BusinessTripAdministration.Commands;
using BusinessTripAdministration.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BusinessTripAdministration.ViewModels
{
    internal class SingleRequestViewModel 
    {
        private string user;
        private string destination;
        private string startDate;
        private string endDate;

        public SingleRequestViewModel(string user, string destination, string startDate, string endDate)
        {
            User = user;
            Destination = destination;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
               
            }
        }

        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = value;
                
            }
        }
        public string StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
               
            }
        }
        public string EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }


        private ICommand acceptCommand;
        private ICommand denyCommand;

        public ICommand AcceptCommand
        {
            get
            {
                if (acceptCommand == null)
                {
                    acceptCommand = new ButtonCommand(
                        param => this.AcceptRequest(),
                        param => this.CanAccept()
                    );
                }
                return acceptCommand;
            }
        }



        public ICommand DenyCommand
        {
            get
            {
                if (denyCommand == null)
                {
                    denyCommand = new ButtonCommand(
                        param => this.DenyRequest(),
                        param => this.CanDeny()
                    );
                }
                return denyCommand;
            }
        }


        public void AcceptRequest()
        {
            MessageBox.Show("Binding Works");
        }
        private bool CanAccept()
        {
            return true;
        }
        public void DenyRequest()
        {

        }
        private bool CanDeny()
        {
            return true;
        }

    }
}
