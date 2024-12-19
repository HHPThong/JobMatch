using FPTJobMatch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace FPTJobMatch.Data
{
    public class ApplicationDBContext:IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<ApplicationJob> apps { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        
    }
}
