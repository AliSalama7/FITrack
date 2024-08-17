namespace FITrack.Models
{
    public class WorkoutExercises
    {
        public Exercise Exercise { get; set; }
        public int ExerciseId {  get; set; }
        public Workout Workout { get; set; }
        public int WorkoutId { get; set; }
        public ICollection<Set>? Sets { get; set; } = new List<Set>();  
        public string? Notes { get; set; }
    }
}
