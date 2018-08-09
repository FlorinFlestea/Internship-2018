using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BusinessTripApplication.Models;
using BusinessTripModels.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using BusinessTripApplication.ViewModels;

namespace BusinessTripApplication.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService tripService = new TripService(new TripRepository());
        private readonly IAreaService areaService = new AreaService(new AreaRepository());
        private readonly IUserService userService = new UserService(new UserRepository());


        private readonly TripRepository Repository = new TripRepository();

        // GET: Trips
        [Authorize]
        public ActionResult Index()
        {
            var identity = User.Identity.Name;
            User user = userService.FindByEmail(identity);

            if (user != null)
            {
                var db = new DatabaseContext();
                Repository.GetAll();
                return View(db.Trips.Where(t => t.User.Id == user.Id));
            }
            else
                return View();
        }

        // GET: Trips/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var trip = Repository.FindById(id);

            if (trip == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details",trip);
        }

        // GET: Trips/Create
        [Authorize]
        public ActionResult Create()
        {
            TripRequestViewModel model = new TripRequestViewModel(areaService);

            return View(model);

        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Exclude = "User, Status")] Trip trip)
        {
            //Get usere from session
            trip.User = new User() { Email = User.Identity.Name };
         
            TripRequestViewModel model = new TripRequestViewModel(ModelState.IsValid, trip, tripService, areaService, userService);

            return View(model);

        }

        // GET: Trips/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trip = Repository.FindById(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete",trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            TripDeleteViewModel model=new TripDeleteViewModel(id, userService, tripService);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}