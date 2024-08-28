namespace FITrack.FiTrack.Domain.DTOS
{
    public class SetDto
    {
        public int SetId { get; set; }
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
        public string? Notes { get; set; }
    }
}
