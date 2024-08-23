using FPTJobMatch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPTJobMatch.Data
{
    public class ApplicationDBContext:IdentityDbContext
    {
        public DbSet<Job> jobs { get; set; }
        public DbSet<ApplicationJob> apps { get; set; }
        public ApplicationDBContext(DbContextOptions options) : base(options) { }

    }
}
