namespace FITrack.FiTrack.Domain.DTOS
{
    public class WorkoutExerciseDto
    {
        public int ExerciseId { get; set; }
        public string? ExerciseName { get; set; }
        public ICollection<SetDto>? Sets { get; set; }
        public string? Notes { get; set; }
    }
}
