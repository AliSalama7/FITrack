namespace FITrack.FiTrack.EF.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagepath;
        private readonly IMapper _mapper;
        public ExerciseService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagepath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
            _mapper = mapper;
        }
        public async Task<ExerciseDetailDto> AddExerciseAsync(ExerciseDto dto)
        {
            if (await _context.Exercises.FirstOrDefaultAsync(x => x.ExerciseName == dto.ExerciseName) is not null)
                return new ExerciseDetailDto { Message = "Exercise is already exists!!", Success = false };
            if (dto.ExercisePhoto is null)
                return new ExerciseDetailDto { Message = "ExercisePhoto is required!!", Success = false };
            if (!FileSettings.AllowedExtensions.Contains(Path.GetExtension(dto.ExercisePhoto.FileName).ToLower()))
                return new ExerciseDetailDto { Message = "Only .png and .jpg and .jpeg images are allowed!", Success = false };
            if (dto.ExercisePhoto.Length > FileSettings.MaxFileSizeinB)
                return new ExerciseDetailDto { Message = "Max allowed size for exercise photo is 1MB!", Success = false };
            var photo = await SavePhoto(dto.ExercisePhoto);
            var exercise = _mapper.Map<Exercise>(dto);
            exercise.ExercisePhoto = photo;
            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();
            return _mapper.Map<ExerciseDetailDto>(exercise);
        }
        public async Task<IEnumerable<ExerciseDetailDto>> GetAllExerciseAsync(MuscleGroup muscleGroup = default)
        {
            var exercises = await _context.Exercises
                .Where(e => e.MuscleGroup == muscleGroup || muscleGroup == default)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ExerciseDetailDto>>(exercises);
        }
        public async Task<ExerciseDetailDto> GetExerciseByIdAsync(int id)
        {
            var exercise = await _context.Exercises.SingleOrDefaultAsync(x => x.ExerciseId == id);
            if (exercise is null)
                return new ExerciseDetailDto { Message = $"Exercise with id {id} is not found!!", Success = false };
            return _mapper.Map<ExerciseDetailDto>(exercise);
        }

        public ExerciseDetailDto DeleteExercise(int id)
        {
            var exercise = _context.Exercises.Find(id);
            if (exercise is null)
                return new ExerciseDetailDto { Message = $"Exercise with id {id} is not found!!", Success = false };
            _context.Remove(exercise);
            var affectedRows = _context.SaveChanges();
            if (affectedRows > 0)
            {
                var photo = Path.Combine(_imagepath, exercise.ExercisePhoto);
                File.Delete(photo);
            }
            return _mapper.Map<ExerciseDetailDto>(exercise);
        }
        private async Task<string> SavePhoto(IFormFile photo)
        {
            var photoName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            var path = Path.Combine(_imagepath, photoName);
            using var stream = File.Create(path);
            await photo.CopyToAsync(stream);
            return photoName;
        }
    }
}
