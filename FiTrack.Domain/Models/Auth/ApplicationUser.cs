namespace FITrack.FiTrack.Domain.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public ICollection<Workout> Workouts { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Nutrition> Nutritions { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
