using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTripAdministration.ViewModels
{
    class RequestsViewModel: Screen
    {
        
        public RequestsViewModel()
        {
            requestList = new List<SingleRequestViewModel>();
            RequestList.Add(new SingleRequestViewModel("Andrei","america","2018","2019"));
            RequestList.Add(new SingleRequestViewModel("Thomas", "georgia", "2018", "2019"));
            RequestList.Add(new SingleRequestViewModel("Cosmin", "acasa", "2018", "2019"));
            RequestList.Add(new SingleRequestViewModel("Tudor", "???", "2018", "2019"));
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


    }
}
