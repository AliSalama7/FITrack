namespace FITrack.DTOS
{
    public class ExerciseDto
    {
        [StringLength(100)]
        public string ExerciseName { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public IFormFile? ExercisePhoto { get; set; }
    }
}
