using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using BusinessTripApplication.Service;
using BusinessTripModels.Models;
using BussinesTripModels.Models;

namespace BusinessTripApplication.Controllers
{
    public class TripsApiController : ApiController
    {
        private readonly TripService tripService = new TripService();
        
        // GET: api/TripsApi
        public IQueryable<Trip> GetTrips()
        {
            return tripService.GetAll().AsQueryable();
        }


        // GET : api/TripsApi/approved
        [Route("api/TripsApi/approved")]
        public IQueryable<Trip> GetTripsApproved()
        {
            return tripService.GetAll().AsQueryable().Where(a => a.Status == 1);
        }

        // GET : api/TripsApi/denied
        [Route("api/TripsApi/denied")]
        public IQueryable<Trip> GetTripsDenied()
        {
            return tripService.GetAll().AsQueryable().Where(a => a.Status == 0);
        }


        // GET : api/TripsApi/pending
        [Route("api/TripsApi/pending")]
        public IQueryable<Trip> GetTripsPending()
        {
            return tripService.GetAll().AsQueryable().Where(a => a.Status > 1);
        }

        // GET: api/TripsApi/5
        [ResponseType(typeof(Trip))]
        public IHttpActionResult GetTrip(int id)
        {
            Trip trip = tripService.FindById(id);
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


            var updatedTrip = tripService.Update(model.Trip);

            return StatusCode(updatedTrip.Id == model.Id ? HttpStatusCode.Accepted : HttpStatusCode.NoContent);

      
        }

        // POST: api/TripsApi
        [ResponseType(typeof(Trip))]
        public IHttpActionResult PostTrip(Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tripService.Add(trip);

            return CreatedAtRoute("DefaultApi", new { id = trip.Id }, trip);
        }

        // DELETE: api/TripsApi/5
        [ResponseType(typeof(Trip))]
        public IHttpActionResult DeleteTrip(int id)
        {
            Trip trip = tripService.FindById(id);
            if (trip == null)
            {
                return NotFound();
            }

           tripService.Remove(trip);

            return Ok(trip);
        }

 

      
    }
}