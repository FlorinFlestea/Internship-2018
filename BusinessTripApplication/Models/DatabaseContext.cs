using System.Data.Entity;
using BusinessTripModels.Models;


namespace BusinessTripApplication.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}