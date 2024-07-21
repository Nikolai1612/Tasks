using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasks.Entities;

namespace Tasks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationTask>()
                .HasOne(t => t.Topic)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.TopicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationTask>()
                .HasOne(t => t.Creator)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationTask>()
                .HasMany(t => t.Images)
                .WithOne(i => i.Task)
                .HasForeignKey(i => i.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationTask>()
                .HasMany(t => t.Solutions)
                .WithOne(s => s.Task)
                .HasForeignKey(s => s.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTaskSolution>()
                .HasKey(uts => new { uts.UserId, uts.TaskId });

            modelBuilder.Entity<UserTaskSolution>()
                .HasOne(uts => uts.User)
                .WithMany(u => u.SolvedTasks)
                .HasForeignKey(uts => uts.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTaskSolution>()
                .HasOne(uts => uts.Task)
                .WithMany(t => t.Solvers)
                .HasForeignKey(uts => uts.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTaskSolution>()
                .HasOne(uts => uts.Solution)
                .WithMany(s => s.UserTaskSolutions)
                .HasForeignKey(uts => uts.SolutionId)
                .OnDelete(DeleteBehavior.Restrict);
        }


        public DbSet<ApplicationTask> Tasks { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserTaskSolution> UserTaskSolutions { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Solution> Solutions { get; set; }

        //public DbSet<Tag> Tags { get; set; }
        //public DbSet<Rating> Ratings { get; set; }
    }
}
