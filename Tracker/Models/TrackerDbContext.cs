using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tracker.Models
{
    public class TrackerDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("COMP");
        }
    }
}