﻿using Microsoft.AspNetCore.Identity;
namespace FITrack.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName {  get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public Gender Gender {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Weight {  get; set; }
        public double Height {  get; set; }
        public ICollection<Workout> Workouts { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Nutrition> Nutritions { get; set; }
        public ICollection<Goal> Goals { get; set; }
    }
}
