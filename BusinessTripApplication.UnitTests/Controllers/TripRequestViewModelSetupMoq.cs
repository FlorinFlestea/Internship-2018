using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using BusinessTripApplication.ViewModels;
using BusinessTripModels.Models;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessTripApplication.UnitTests.Controllers
{
    public static class TripRequestViewModelSetupMoq
    {
        public static void GetListItems(Mock<ITripRequestViewModel> MockRequest)
        {
            MockRequest.Setup(mock => mock.GetSelectItems(It.IsAny<IAreaService>())).Returns(
               (IAreaService areaService) =>
               {
                   IList<Area> areas = areaService.FindAll();

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
               });
        }

        public static void AddTrip(Mock<ITripRequestViewModel> MockRequest)
        {
            MockRequest.Setup(mock => mock.AddTrip(It.IsAny<Trip>(), It.IsAny<ITripService>(), It.IsAny<IAreaService>(), It.IsAny<IUserService>())).Callback(
                (Trip trip, ITripService tripService, IAreaService areaService, IUserService userService) =>
                {
                    trip.User = userService.FindByEmail(trip.User.Email);
                    trip.Area = areaService.FindById(trip.Area.Id);
                    tripService.Add(trip);
                });
        }
    }
}
