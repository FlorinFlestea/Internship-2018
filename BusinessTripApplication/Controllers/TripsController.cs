using System.Linq;
using System.Net;
using System.Web.Mvc;
using BusinessTripModels;
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
            var returnList = Repository.GetAll().Where(task => task.User.Email == identity);

            return View(returnList);
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
            return View(trip);
        }

        // GET: Trips/Create
        [Authorize]
        public ActionResult Create()
        {
            TripRequestViewModel model = new TripRequestViewModel(areaService);
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
        [Authorize]
        public ActionResult Create([Bind(Exclude = "User, Status")] Trip trip)
        {
            //Get usere from session
            trip.User = new User() { Email = User.Identity.Name };

            TripRequestViewModel model = new TripRequestViewModel(ModelState.IsValid, trip, tripService, areaService, userService);
            if (model.Status)
                return View(model);
            else
                return RedirectToRoute("~/Shared/Error");
        }

        // GET: Trips/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
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
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,PmName,ClientName,StartingDate,EndDate,ProjectName,ProjectNumber,TaskNumber,ClientLocation,DepartureLocation,Transportation,NeedOfPhone,NeedOfBankCard,Accommodation,Comments,Approved")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                Repository.Update(trip);
                return RedirectToAction("Index");
            }
            return View(trip);
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
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Repository.Remove(Repository.FindById(id));
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