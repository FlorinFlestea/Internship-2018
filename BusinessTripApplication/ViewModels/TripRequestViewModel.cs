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
    public class TripRequestViewModel
    {
        public Trip Trip { get; set; }

        public string Message { get; }

        public bool Status { get; }
        public IEnumerable<SelectListItem> Areas { get; }


        //Constructor for GET
        public TripRequestViewModel(IAreaService areaService)
        {
            Areas = new List<SelectListItem>();
            Trip = new Trip();

            try
            {
                Areas = GetSelectItems(areaService);
            }
            catch (DatabaseException e)
            {
                Message = e.Message;
                Status = false;
                return;
            }
            catch (Exception e)
            {
                Message = e.Message;
                Status = false;
                return;
            }

            Trip = new Trip();
            Status = true;
        }

        //Constructor for POST
        public TripRequestViewModel(bool modelState, Trip trip, ITripService tripService, IAreaService areaService, IUserService userService)
        {
            Areas = new List<SelectListItem>();
            Trip = new Trip();

            if (modelState)
            {
                try
                {
                    AddTrip(trip, tripService, areaService, userService);
                    SendEmail(trip.Id, userService, tripService);
                    Areas = GetSelectItems(areaService);
                }
                catch (DatabaseException e)
                {
                    Message = e.Message;
                    Status = false;
                    return;
                }
                catch (Exception e)
                {
                    Message = e.Message;
                    Status = false;
                    return;
                }

                //Send email to Admin

                Message = "Submit successfully done. An admit will be notified!";
                Status = true;
            }
            else
            {
                try
                {
                    Areas = GetSelectItems(areaService);
                }
                catch (DatabaseException e)
                {
                    Message = e.Message;
                    Status = false;
                    return;
                }
                catch (Exception e)
                {
                    Message = e.Message;
                    Status = false;
                    return;
                }
                Message = "Invalid request!";
                Status = false;
            }
        }

        private IList<SelectListItem> GetSelectItems(IAreaService areaService)
        {
            IList<Area> areas = areaService.FindAll();

            if (areas.Count == 0)
                throw new Exception("You can't submit trips because Areas doesn't exists.");

            IList<SelectListItem> selectAreas = new List<SelectListItem>();
            foreach (Area element in areas)
            {
                selectAreas.Add(new SelectListItem()
                {
                    Text = element.Name,
                    Value = element.Id.ToString()
                });
            }

            return selectAreas;
        }
        private void AddTrip(Trip trip, ITripService tripService, IAreaService areaService, IUserService userService)
        {

            trip.User = userService.FindByEmail(trip.User.Email);
            if (trip.User == default(User))
                throw new Exception("User doesn't exists.");

            trip.Area = areaService.FindById(trip.Area.Id);
            if (trip.Area == default(Area))
                throw new Exception("Area doesn't exists.");
            tripService.Add(trip);
        }

        private void SendEmail(int id, IUserService service, ITripService tripService)
        {
            Trip trip = tripService.FindById(id);
            EmailSender emailSender = new EmailSender();
            string message = "There is a new Trip request with id "+id+" from user "
                             +trip.User.Email+"<br/>"+
                "Starting date: "+ trip.StartingDate+"<br/>"+
            "End date: " + trip.EndDate+"<br/>";
            List<User> admins = service.FindAllAdmins().ToList();
            
            foreach (User admin in admins)
            {
                emailSender.SendEmail(admin.Email, "New Trip Request", message);
            }
            
        }
    }
}