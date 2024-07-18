using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tasks.Entities;

namespace Tasks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationTask> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}
