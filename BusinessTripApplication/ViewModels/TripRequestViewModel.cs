using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessTripApplication.Exception;

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

            Trip = new Trip();
            Status = true;
        }

        //Constructor for POST
        public TripRequestViewModel(bool modelState, Trip trip, ITripService tripService, IAreaService areaService, IUserService userService)
        {
            if (modelState)
            {
                try
                {
                    AddTrip(trip, tripService, areaService, userService);
                    Areas = GetSelectItems(areaService);
                }
                catch (DatabaseException e)
                {
                    Message = e.Message;
                    Status = false;
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
                Message = "Invalid request!";
                Status = false;
            }
        }

        private IList<SelectListItem> GetSelectItems(IAreaService areaService)
        {
            IList<Area> areas = new List<Area>();
            try
            {
                areas = areaService.FindAll();
            }
            catch
            {
                throw;
            }

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
            try
            {
                trip.User = userService.FindByEmail(trip.User.Email);
                trip.Area = areaService.FindById(trip.Area.Id);
                tripService.Add(trip);
            }
            catch
            {
                throw;
            }
        }
    }
}