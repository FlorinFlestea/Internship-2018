using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using BusinessTripApplication.UnitTests.Repository;
using BusinessTripApplication.ViewModels;
using BusinessTripModels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BusinessTripApplication.UnitTests.Controllers
{
    [TestClass]
    public class TripRequestViewModelTests
    {

        [TestMethod]
        public void GetSelectItems_Returns_SelectListItems_With_AllAreas()
        {
            //Arrange
            IList<Area> areas = new List<Area>()
            {
                new Area(){ Id=1, Name="ASD"},
                new Area(){Id=2, Name="as"}
            };

            Mock<IAreaRepository> MockAreaRepository = new Mock<IAreaRepository>();
            AreaRepositorySetupMoq.FindAll(MockAreaRepository, areas);
            IAreaService areaService = new AreaService(MockAreaRepository.Object);          

            Mock<ITripRequestViewModel> MockRequestModel = new Mock<ITripRequestViewModel>();
            TripRequestViewModelSetupMoq.GetListItems(MockRequestModel);

            //Act
            IList<SelectListItem> selectListItems = MockRequestModel.Object.GetSelectItems(areaService);

            //Assert
            Assert.AreEqual(selectListItems.Count, areas.Count);
            Assert.AreEqual(selectListItems[0].Value, areas[0].Id.ToString());
            Assert.AreEqual(selectListItems[1].Text, areas[1].Name);
        }

        [TestMethod]
        public void AddTrip_Add()
        {
            //Arrange
            IList<Area> areas = new List<Area>()
            {
                new Area(){ Id=1, Name="ASD"},
                new Area(){Id=2, Name="as"}
            };

            Mock<IAreaRepository> MockAreaRepository = new Mock<IAreaRepository>();
            AreaRepositorySetupMoq.FindAll(MockAreaRepository, areas);
            IAreaService areaService = new AreaService(MockAreaRepository.Object);

            IList<Trip> trips = new List<Trip>()
            {
                new Trip()
            };

            Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
            TripRepositorySetupMoq.Add(MockTripRepository, trips);
            ITripService tripService = new TripService(MockTripRepository.Object);

            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email="giani"}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindByEmail(MockUserRepository, users);
            IUserService userService = new UserService(MockUserRepository.Object);

            Mock<ITripRequestViewModel> MockRequestModel = new Mock<ITripRequestViewModel>();
            TripRequestViewModelSetupMoq.AddTrip(MockRequestModel);

            //Act
            Trip trip = new Trip() { Id = 2 , User =  new User() {Email = "giani"}, Area = new Area() { Id = 2} };
            MockRequestModel.Object.AddTrip(trip, tripService, areaService, userService);

            //Assert
            Assert.AreEqual(trips.Count, 2);
            Assert.AreEqual(trips[1].User.Id, 1);
            Assert.AreEqual(trips[1].Area.Name, "as");
        }
    }
}
