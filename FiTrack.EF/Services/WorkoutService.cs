namespace FITrack.FiTrack.EF.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public WorkoutService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<WorkoutDto> AddWorkoutAsync(WorkoutDto dto)
        {
            if (await _context.Workouts.FirstOrDefaultAsync(x => x.WorkoutName == dto.WorkoutName) is not null)
                return new WorkoutDto { Message = "Workout is already added!!", Success = false };
            var workout = _mapper.Map<Workout>(dto);
            workout.UserId = _httpContextAccessor.HttpContext.User.FindFirstValue("uid");
            await _context.Workouts.AddAsync(workout);
            _context.SaveChanges();
            return _mapper.Map<WorkoutDto>(workout);
        }
        public async Task<WorkoutDto> AddExercisesToWorkoutAsync(int workoutId, WorkoutExerciseDto dto)
        {
            var workout = await _context.Workouts
                .Include(w => w.Exercises)
                .ThenInclude(we => we.Exercise)
                .FirstOrDefaultAsync(w => w.Id == workoutId);
            if (workout is null)
                return new WorkoutDto { Message = $"Workout with id : {workoutId} is not found!!", Success = false };
            if (workout.Exercises.FirstOrDefault(we => we.ExerciseId == dto.ExerciseId) is not null)
                return new WorkoutDto { Message = "This exercise is already exists in this workout!!", Success = false };
            if (await _context.Exercises.FindAsync(dto.ExerciseId) is null)
                return new WorkoutDto { Message = $"Exercise with id : {dto.ExerciseId} is not found!!", Success = false };
            var workoutExercise = _mapper.Map<WorkoutExercises>(dto);
            workout.Exercises.Add(workoutExercise);
            await _context.SaveChangesAsync();
            return _mapper.Map<WorkoutDto>(workout);
        }
        public async Task<WorkoutDto> AddSetsToExerciseAsync(SetDto dto)
        {
            var workout = await _context.Workouts
             .Include(w => w.Exercises)
             .ThenInclude(we => we.Exercise)
             .Include(w => w.Exercises)
             .ThenInclude(we => we.Sets)
             .FirstOrDefaultAsync(w => w.Id == dto.WorkoutId);
            if (workout is null)
                return new WorkoutDto { Message = $"Workout with id : {dto.WorkoutId} is not found!!", Success = false };
            var Exercise = workout.Exercises.FirstOrDefault(we => we.ExerciseId == dto.ExerciseId);
            if (Exercise is null)
                return new WorkoutDto { Message = $"Exercise with id : {dto.ExerciseId} is not found!!", Success = false };
            Exercise.Sets.Add(_mapper.Map<Set>(dto));
            _context.SaveChanges();
            return workout.toDto();
        }
        public async Task<IEnumerable<WorkoutDto>> GetWorkoutsAsync(string UserId)
        {
            var workouts = await _context.Workouts
              .Include(w => w.Exercises)
              .ThenInclude(we => we.Exercise)
              .Include(w => w.Exercises)
              .ThenInclude(we => we.Sets)
              .Where(w => w.UserId == UserId)
              .ToListAsync();
            return workouts.Select(w => w.toDto()).ToList();
        }
        public WorkoutDto DeleteWorkout(int workoutId)
        {
            var workout = _context.Workouts
                .Include(w => w.Exercises)
                .ThenInclude(we => we.Exercise)
                .FirstOrDefault(w => w.Id == workoutId);
            if (workout is null)
                return new WorkoutDto { Message = $"Workout with id : {workoutId} is not found!!", Success = false };
            _context.Remove(workout);
            _context.SaveChanges();
            return _mapper.Map<WorkoutDto>(workout);
        }
        public WorkoutDto DeleteExerciseFromWorkout(int workoutId, int exerciseId)
        {
            var workout = _context.Workouts
                .Include(w => w.Exercises)
                .ThenInclude(we => we.Exercise)
                .FirstOrDefault(w => w.Id == workoutId);
            if (workout is null)
                return new WorkoutDto { Message = $"Workout with id : {workoutId} is not found!!", Success = false };
            var exercise = workout.Exercises.FirstOrDefault(we => we.ExerciseId == exerciseId);
            if (exercise is null)
                return new WorkoutDto { Message = $"Exercise with  id : {exerciseId} is not found!!", Success = false };
            workout.Exercises.Remove(exercise);
            _context.SaveChanges();
            return _mapper.Map<WorkoutDto>(workout);
        }
        public WorkoutDto DeleteSetFromExercise(int workoutId, int exerciseId, int setId)
        {
            var workout = _context.Workouts
            .Include(w => w.Exercises)
            .ThenInclude(we => we.Exercise)
            .Include(w => w.Exercises)
            .ThenInclude(we => we.Sets)
            .FirstOrDefault(w => w.Id == workoutId);
            if (workout is null)
                return new WorkoutDto { Message = $"Workout with id : {workoutId} is not found!!", Success = false };
            var exercise = workout.Exercises.FirstOrDefault(we => we.ExerciseId == exerciseId);
            if (exercise is null)
                return new WorkoutDto { Message = $"Exercise with  id : {exerciseId} is not found!!", Success = false };
            var set = exercise.Sets.FirstOrDefault(s => s.SetId == setId);
            if (set is null)
                return new WorkoutDto { Message = $"Set with  id : {setId} is not found!!", Success = false };
            exercise.Sets.Remove(set);
            _context.SaveChanges();
            return workout.toDto();
        }
    }
}
