﻿using System.Data.Entity;
using BusinessTripModels;

namespace BusinessTripApplication.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Area> Areas { get; set; }
    }
}