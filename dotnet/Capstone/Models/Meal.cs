using System.Collections.Generic;

namespace Capstone.Models
{
    public class Meal 
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }

        public List<Recipe> RecipesList { get; set; } = new List<Recipe>();

    }
}
