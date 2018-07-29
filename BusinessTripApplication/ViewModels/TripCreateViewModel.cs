using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessTripApplication.ViewModels
{
    public class TripCreateViewModel
    {
        public TripCreateViewModel(IAreaService areaService)
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
            Title = "Submit a trip";
            Status = true;
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
                trip.User = userService.FindByEmail(trip.User);
                trip.Area = areaService.FindById(trip.Area);
                tripService.Add(trip);
            }
            catch
            {
                throw;
            }
        }

        public TripCreateViewModel(bool modelState, Trip trip, ITripService tripService, IAreaService areaService, IUserService userService)
        {
            if(modelState)
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

        public Trip Trip { get; }
        public string Message { get; }
        public bool Status { get; }
        public IEnumerable<SelectListItem> Areas { get; }
        public string Title { get; }
    }
}