using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.ViewModels;

namespace BusinessTripApplication.Controllers
{
    public class TripsController : Controller
    {

        private readonly TripRepository Repository = new TripRepository();

        // GET: Trips
        public ActionResult Index()
        {
            var identity = User.Identity.Name;
            var returnList = Repository.GetAll().Where(task => task.User.Email == identity);

            return View(returnList);
        }

        // GET: Trips/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PmName,ClientName,StartingDate,EndDate,ProjectName,ProjectNumber,TaskNumber,ClientLocation,DepartureLocation,Transportation,NeedOfPhone,NeedOfBankCard,Accommodation,Comments")] Trip trip)
        {
            var model = new TripRequestViewModel();
            //TO BE IMPLEMENTED
            //var model = new TripRequestViewModel(ModelState.IsValid, Trip, service);


            return View(model);
        }

        // GET: Trips/Edit/5
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