using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BusinessTripApplication.Models;
using BusinessTripModels.Exception;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Repository
{
    public class TripRepository : ITripRepository
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Trip Add(Trip addedTrip)
        {
            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    context.Trips.Add(addedTrip);
                    context.Entry(addedTrip.Area).State = EntityState.Modified;
                    context.Entry(addedTrip.User).State = EntityState.Modified;
                    context.Entry(addedTrip).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to database!\n");
            }

            return addedTrip;
        }

        public Trip FindById(int? id)
        {
            Trip foundTrip;
            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    foundTrip = context.Trips.Include(x => x.Area).Include(x => x.User).FirstOrDefault(x=> x.Id == id);
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to database!\n");
            }

            return foundTrip;
        }

        public void Remove(Trip deleteTrip)
        {
            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    context.Trips.Attach(deleteTrip);
                    context.Trips.Remove(deleteTrip);
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to database!\n");
            }
        }

        public IList<Trip> GetAll()
        {
            IList<Trip> trips = new List<Trip>();

            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    trips = context.Trips.ToList();
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to database!\n");
            }

            return trips;
        }

        public Trip Update(Trip updatedTrip)
        {
            Trip update = new Trip();
            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    update = context.Trips.SingleOrDefault(trip => trip.Id == updatedTrip.Id);
                    

                    if (update == null)
                        throw new DatabaseException("Trip doesn't exists!");

                    context.Entry(update).CurrentValues.SetValues(updatedTrip);
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to database!\n");
            }

            return update;
        }
    }
}