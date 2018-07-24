using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.ViewModels
{
    public class TripRequestViewModel
    {
        public Trip Trip { get; set; }

        public string Message { get; }

        public bool Status { get; }

        
        public TripRequestViewModel()
        {
            Status = false;
           
        }

        public TripRequestViewModel(bool modelState, Trip trip, IUserService service)
        {
             //TO BE IMPLEMENTED
        }

    }
}