namespace FITrack.FiTrack.Domain.IServices
{
    public interface IExerciseService
    {
        Task<ExerciseDetailDto> AddExerciseAsync(ExerciseDto dto);
        Task<IEnumerable<ExerciseDetailDto>> GetAllExerciseAsync(MuscleGroup muscleGroup = 0);
        Task<ExerciseDetailDto> GetExerciseByIdAsync(int id);
        ExerciseDetailDto DeleteExercise(int id);
    }
}
