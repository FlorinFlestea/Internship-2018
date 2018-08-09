using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessTripModels.Exception;
using BusinessTripModels.Models;
using System;
using System.Linq;
using BusinessTripApplication.Server;

namespace BusinessTripApplication.ViewModels
{
    public class TripDeleteViewModel
    {
        public Trip Trip { get; set; }
        
        //Constructor for GET
        public TripDeleteViewModel(int id, IUserService service, ITripService tripService)
        {
            Trip = tripService.FindById(id);
            DeleteTrip(Trip, service, tripService);
        }

        private void DeleteTrip(Trip trip, IUserService service, ITripService tripService)
        {
            SendEmail(Trip, service, tripService);
            tripService.Remove(trip);
        }

        private void SendEmail(Trip trip, IUserService service, ITripService tripService)
        {
            EmailSender emailSender = new EmailSender();
            string message = "Deleted Trip request with id "+trip.Id+" from user "
                             +trip.User.Email+"<br/>"+
                "Starting date: "+ trip.StartingDate+"<br/>"+
            "End date: " + trip.EndDate+"<br/>";
            List<User> admins = service.FindAllAdmins().ToList();
            
            foreach (User admin in admins)
            {
                emailSender.SendEmail(admin.Email, "Deleted Trip Request", message);
            }
            
        }
    }
}