using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessTripApplication.Models;
using BusinessTripApplication.ViewModels;

namespace BusinessTripApplication.Controllers
{
    public class TripsController : Controller
    {
        private BusinessContext db = new BusinessContext();

        // GET: Trips
        public ActionResult Index(string option,string search)
        {
            if (option == "Name")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Trips.Where(x=> x.PmName == search || search == null).ToList());
            }
            else if (option == "ProjectName")
            {
                return View(db.Trips.Where(x=> x.ProjectName == search || search == null).ToList());
            }
            else if(option =="ProjectNumber")
            {
                return View(db.Trips.Where(x=> x.ProjectNumber== search || search == null).ToList());
            }

            return View(db.Trips);
        }

        


        
        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
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
            Trip trip = db.Trips.Find(id);
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
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
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
            Trip trip = db.Trips.Find(id);
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
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
