namespace FITrack.FiTrack.Domain.Models.Business
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        [StringLength(100)]
        public string ExerciseName { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public string ExercisePhoto { get; set; }
    }
}
