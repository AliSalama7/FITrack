namespace FITrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _workoutService;
        public ExerciseController(IExerciseService workoutService)
        {
            _workoutService = workoutService;
        }
        [HttpPost("AddExercise")]
        public async Task<IActionResult> AddExerciseAsync([FromForm] ExerciseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _workoutService.AddExerciseAsync(dto);
            if(!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpGet("GetExercises")]
        public async Task<IActionResult> GetExercisesAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var exercises = await _workoutService.GetAllExerciseAsync();
            return Ok(exercises);
        }
        [HttpGet("GetExercisesByMuscleGroup/{muscleGroup}")]
        public async Task<IActionResult> GetExercisesByMuscleGroupAsync(MuscleGroup muscleGroup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var exercises = await _workoutService.GetAllExerciseAsync(muscleGroup);
            return Ok(exercises);
        }
        [HttpGet("GetExercise/{id}")]
        public async Task<IActionResult> GetExerciseByIdAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var exercise = await _workoutService.GetExerciseByIdAsync(id);
            if (!exercise.Success)
                return BadRequest(exercise.Message);
            return Ok(exercise);
        }
        [HttpDelete("DeleteExercise/{id}")]
        public IActionResult DeleteExercise(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _workoutService.DeleteExercise(id);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }
        
    }
}
