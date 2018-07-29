using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using BusinessTripApplication.ViewModels;

namespace BusinessTripApplication.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService tripService = new TripService();
        private readonly IAreaService areaService = new AreaService();
        private readonly IUserService userService = new UserService();

        // GET: Trips/Create
        public ActionResult Create()
        {
            TripCreateViewModel model = new TripCreateViewModel(areaService);
            if (model.Status)
                return View(model);
            else
                return RedirectToRoute("~/Shared/Error");
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "User, Status")] Trip trip)
        {
            //Get usere from session
            trip.User = new User() { Id = 2, Name = "TempUser", Email = "dragoscojanu97@yahoo.ro" };
            TripCreateViewModel model = new TripCreateViewModel(ModelState.IsValid, trip, tripService, areaService, userService);
            return View(model);
            if (model.Status)
                return View(model);
            else
                return RedirectToRoute("~/Shared/Error");
        }
    }
}
