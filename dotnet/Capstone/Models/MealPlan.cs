using System.Collections.Generic;

namespace Capstone.Models
{
    public class MealPlan
    {
        public int MealPlanId { get; set; }
        public string MealPlanName { get; set; }
        public string MealPlanDescription { get; set; }

        public List<Recipe> RecipesList { get; set; }
        public int MealId { get; set; }
        public string MealName { get; set; }
    }
}
