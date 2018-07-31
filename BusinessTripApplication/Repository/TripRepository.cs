using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BusinessTripApplication.Models;

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
            catch (Exception e)
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
                    foundTrip = context.Trips.Find(id);
                }
            }
            catch (Exception e)
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
                    context.Trips.Remove(deleteTrip);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
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
            catch (Exception e)
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

                    update = updatedTrip;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to database!\n");
            }

            return update;
        }
    }
}