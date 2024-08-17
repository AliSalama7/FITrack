namespace FITrack.Models
{
    public class Workout 
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public WorkoutType WorkoutType { get; set; }
        [StringLength(50)]
        public string WorkoutName { get; set; }
        public ICollection<WorkoutExercises>? Exercises { get; set; }
        public int? Duration {  get; set; }
        public int? CaloriesBurned {  get; set; }
        public DateTime Date { get; set; }
        [StringLength(350)]
        public string? Notes { get; set; }
    }
}
