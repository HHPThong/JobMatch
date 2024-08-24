using FPTJobMatch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPTJobMatch.Data
{
    public class ApplicationDBContext:IdentityDbContext
    {
        public DbSet<Status> status { get; set; }
        public DbSet<TimeWork> timeWork { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<ApplicationJob> apps { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, NameStatus = "Pending" },
                new Status { Id = 2, NameStatus = "Accepted" },
                new Status { Id = 3, NameStatus = "Rejected" }
                );
            modelBuilder.Entity<TimeWork>().HasData(
                new TimeWork { ID = 1, Type ="Full Time"},
                new TimeWork { ID = 2 ,Type = "Part Time"}
                );
            modelBuilder.Entity<Job>().HasData(
                new Job { ID = 1, Name = "Software Engineer", Company = "FPT Company", Salary = 40000, TimeWorkID = 1, Description = "", Request ="" }, 
                new Job { ID = 2, Name = "Data Scientist"   , Company = "FPT Company", Salary = 50000, TimeWorkID = 2, Description = "", Request ="" }
                );
            modelBuilder.Entity<ApplicationJob>().HasData(
                new ApplicationJob { Id = 1, Description = "", StatusID = 1, JobID = 1}
                );
        }


    }
}
