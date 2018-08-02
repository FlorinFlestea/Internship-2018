using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTripAdministration.ViewModels
{
    class RequestsViewModel: Conductor<object>
    {
        
        public RequestsViewModel()
        {
            requestList = new List<SingleRequestViewModel>();   
        }
        private List<SingleRequestViewModel> requestList;
        public List<SingleRequestViewModel> RequestList
        {
            get
            {
                return requestList;
            }
            set
            {
                requestList = value;
                NotifyOfPropertyChange(() => RequestList);
            }
        }

        private void GetRequestsFromDatabase(List<SingleRequestViewModel> requestList)
        {

        }

    }
}
