using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessTripModels;

namespace BusinessTripApplication.Models
{
    public class UpdateTripModel
    {
        public int Id { get; set; }
        public Trip Trip { get; set; }

        public UpdateTripModel(int id, Trip trip)
        {
            Trip = trip;
            Id = id;
        }
    }
}