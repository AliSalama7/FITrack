using Microsoft.AspNetCore.Authorization;
namespace FITrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }
        [Authorize(Roles = "User")]
        [HttpPost("AddWorkout")]
        public async Task<IActionResult> AddWorkoutAsync([FromBody ] WorkoutDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Workout = await _workoutService.AddWorkoutAsync(dto);
            if(!Workout.Success)
                return BadRequest(Workout.Message);
            return Ok(Workout);
        }
        [HttpPost("{workoutId}/exercises")]
        public async Task<IActionResult> AddExercisesToWorkoutAsync(int workoutId, [FromForm] WorkoutExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           var workout =  await _workoutService.AddExercisesToWorkoutAsync(workoutId, exerciseDto);
            if (!workout.Success)
                return BadRequest(workout.Message);
            return Ok(workout);
        }
        [HttpPost("AddSetsToExercise")]
        public async Task<IActionResult> AddSetsToExerciseAsync([FromForm] SetDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var workout = await _workoutService.AddSetsToExerciseAsync(dto);
            if (!workout.Success)
                return BadRequest(workout.Message);
            return Ok(workout);
        }
        [HttpGet("GetWorkouts")]
        public async Task<IActionResult> GetWorkoutsAsync([FromForm]string UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var workouts = await _workoutService.GetWorkoutsAsync(UserId);
            return Ok(workouts);
        }
        [HttpDelete("DeleteWorkout/{id}")]
        public IActionResult DeleteWorkout(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var workout = _workoutService.DeleteWorkout(id);
            if (!workout.Success)
                return BadRequest(workout.Message);
            return Ok(workout);
        }
        [HttpDelete("DeleteExerciseFromWorkout/{workoutId}/{exerciseId}")]
        public IActionResult DeleteExerciseFromWorkout(int workoutId,int exerciseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var workout = _workoutService.DeleteExerciseFromWorkout(workoutId , exerciseId);
            if (!workout.Success)
                return BadRequest(workout.Message);
            return Ok(workout);
        }
        [HttpDelete("DeleteSetFromExercise/{workoutId}/{exerciseId}/{setId}")]
        public IActionResult DeleteSetFromExercise(int workoutId, int exerciseId,int setId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var workout = _workoutService.DeleteSetFromExercise(workoutId, exerciseId,setId);
            if (!workout.Success)
                return BadRequest(workout.Message);
            return Ok(workout);
        }
    }
}
