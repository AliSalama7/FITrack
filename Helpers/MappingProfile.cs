namespace FITrack.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModel, ApplicationUser> ();
            CreateMap<Exercise, ExerciseDetailDto>();
            CreateMap<ExerciseDto, Exercise>()
                .ForMember(src => src.ExercisePhoto, opt => opt.Ignore());
            CreateMap<WorkoutDto, Workout>()
                .ForMember(dest => dest.Exercises, opt => opt.Ignore());
            CreateMap<WorkoutExercises, WorkoutExerciseDto>()
                .ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.Exercise.ExerciseName));
            CreateMap<SetDto, Set>();
            CreateMap<WorkoutExerciseDto, WorkoutExercises>()
                .ForMember(dest => dest.Exercise, opt => opt.Ignore()) 
                .ForMember(dest => dest.Workout, opt => opt.Ignore());
            CreateMap<Workout, WorkoutDto>()
                .ForMember(dest => dest.WorkoutType, opt => opt.MapFrom(src => src.WorkoutType.ToString()))
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises));
        }
    }
}
