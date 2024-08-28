namespace FITrack.Helpers
{
    public static class WorkoutMapping
    {
        public static WorkoutDto toDto(this Workout workout)
        {
            return new WorkoutDto()
            {
                Id = workout.Id,
                WorkoutName = workout.WorkoutName,
                WorkoutType = workout.WorkoutType.ToString(),
                Duration = workout.Duration,
                CaloriesBurned = workout.CaloriesBurned,
                Date = workout.Date,
                Notes = workout.Notes,
                Exercises = workout.Exercises.Select(we => new WorkoutExerciseDto
                {
                    ExerciseId = we.ExerciseId,
                    ExerciseName = we.Exercise.ExerciseName,
                    Sets = we.Sets.Select(s => new SetDto
                    {
                        SetId = s.SetId,
                        Reps = s.Reps,
                        Weight = s.Weight,
                        Notes = s.Notes,
                        ExerciseId = s.ExerciseId,
                        WorkoutId = s.ExerciseId
                    }).ToList(),
                    Notes = we.Notes
                }).ToList()
            };
        }
    }
}
