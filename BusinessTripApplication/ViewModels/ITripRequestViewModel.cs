using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using BusinessTripModels.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessTripApplication.ViewModels
{
    public interface ITripRequestViewModel
    {
        void AddTrip(Trip trip, ITripService tripService, IAreaService areaService, IUserService userService);
        IList<SelectListItem> GetSelectItems(IAreaService areaService);
    }
}