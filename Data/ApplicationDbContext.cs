using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace FITrack.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Nutrition> Meals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "User", ConcurrencyStamp = Guid.NewGuid().ToString()  },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "SuperAdmin", NormalizedName = "SuperAdmin", ConcurrencyStamp = Guid.NewGuid().ToString() }
            );
            modelBuilder.Entity<WorkoutExercises>().HasKey(we => new { we.WorkoutId , we.ExerciseId });

            modelBuilder.Entity<Set>()
                .HasOne(s => s.workoutExercises)
                .WithMany(we => we.Sets)
                .HasForeignKey(s => new { s.WorkoutId, s.ExerciseId });
        }                                               
    }
}
