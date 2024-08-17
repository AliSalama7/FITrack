namespace FITrack.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [StringLength(50)]
        public string ActivityType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; } 
        public double? Distance { get; set; } 
        public int? CaloriesBurned { get; set; }
    }
}
