using Microsoft.EntityFrameworkCore;
using JobListingAPI.Models;

namespace JobListingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .Property(j => j.Salary)
                .HasPrecision(18, 2); // Ensures decimal(18,2) in SQL Server
        }
    }
}
