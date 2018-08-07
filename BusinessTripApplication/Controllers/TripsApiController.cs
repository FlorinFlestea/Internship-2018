using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using BusinessTripApplication.Models;
using BusinessTripModels;
using BussinesTripModels;

namespace BusinessTripApplication.Controllers
{
    public class TripsApiController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/TripsApi
        public IQueryable<Trip> GetTrips()
        {
            return db.Trips;
        }


        // GET : api/TripsApi/approved
        [Route("api/TripsApi/approved")]
        public IQueryable<Trip> GetTripsApproved()
        {
            return db.Trips.Where(a => a.Status == 1);
        }

        // GET : api/TripsApi/denied
        [Route("api/TripsApi/denied")]
        public IQueryable<Trip> GetTripsDenied()
        {
            return db.Trips.Where(a => a.Status == 0);
        }


        // GET : api/TripsApi/pending
        [Route("api/TripsApi/pending")]
        public IQueryable<Trip> GetTripsPending()
        {
            return db.Trips.Where(a => a.Status > 1);
        }

        // GET: api/TripsApi/5
        [ResponseType(typeof(Trip))]
        public IHttpActionResult GetTrip(int id)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        // PUT: api/TripsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrip(UpdateTripModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Trip trip = model.Trip;
            int id = model.Id;

            if (id != trip.Id)
            {
                return BadRequest();
            }

            db.Entry(trip).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TripsApi
        [ResponseType(typeof(Trip))]
        public IHttpActionResult PostTrip(Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trips.Add(trip);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trip.Id }, trip);
        }

        // DELETE: api/TripsApi/5
        [ResponseType(typeof(Trip))]
        public IHttpActionResult DeleteTrip(int id)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            db.Trips.Remove(trip);
            db.SaveChanges();

            return Ok(trip);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TripExists(int id)
        {
            return db.Trips.Count(e => e.Id == id) > 0;
        }
    }
}