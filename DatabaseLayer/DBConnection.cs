using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;


namespace DatabaseLayer
{
    public class TimeTrackingDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource=C:\SQLite\timeTracking.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<TrackedTime>()
            //  .HasKey(tt => new { tt.Id});
        }

        public DbSet<TrackedTime> TrackedTimes { get; set; }
    }
}