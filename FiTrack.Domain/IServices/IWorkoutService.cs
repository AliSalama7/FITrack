namespace FITrack.FiTrack.Domain.IServices
{
    public interface IWorkoutService
    {
        Task<WorkoutDto> AddWorkoutAsync(WorkoutDto dto);
        Task<WorkoutDto> AddExercisesToWorkoutAsync(int workoutId, WorkoutExerciseDto dto);
        Task<WorkoutDto> AddSetsToExerciseAsync(SetDto dto);
        Task<IEnumerable<WorkoutDto>> GetWorkoutsAsync(string UserId);
        WorkoutDto DeleteWorkout(int workoutId);
        WorkoutDto DeleteExerciseFromWorkout(int workoutId, int exerciseId);
        WorkoutDto DeleteSetFromExercise(int workoutId, int exerciseId, int setId);
    }
}
