namespace FITrack.DTOS
{
    public class ExerciseDetailDto
    {   
        public int ExerciseId { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; } = true;
        public string ExerciseName { get; set; }
        public string MuscleGroup { get; set; }
        public string ExercisePhoto { get; set; }
    }
}
