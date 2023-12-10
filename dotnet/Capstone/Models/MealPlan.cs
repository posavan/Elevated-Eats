using System.Collections.Generic;

namespace Capstone.Models
{
    public class MealPlan
    {
        public int MealPlanId { get; set; } = 0;
        public string MealPlanName { get; set; }
        public string MealPlanDescription { get; set; }
        public int UserId { get; set; }

        public List<Meal> MealList { get; set; }
    }
}
