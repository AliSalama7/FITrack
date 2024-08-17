namespace FITrack.DTOS
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; } = true;
        public string WorkoutName {  get; set; }
        public string WorkoutType {  get; set; }
        public int? Duration {  get; set; }
        public int? CaloriesBurned { get; set; }
        public DateTime Date {  get; set; } = DateTime.Now;
        public string? Notes {  get; set; }
        public ICollection<WorkoutExerciseDto>? Exercises { get; set; }
    }
}
