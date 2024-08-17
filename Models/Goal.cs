namespace FITrack.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string UserId {  get; set; } 
        public ApplicationUser User { get; set; }
        public string? GoalType { get; set; }
        public double Target { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? CurrentProgress { get; set; }
        public string? Status { get; set; } 
    }
}
