namespace FITrack.Models
{
    public class Nutrition
    {
        public int NutritionId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        [StringLength(100)]
        public string FoodItem { get; set; }
        public int Calories { get; set; }
        public double Protein { get; set; } 
        public double Carbohydrates { get; set; } 
        public double Fats { get; set; } 
    }
}
