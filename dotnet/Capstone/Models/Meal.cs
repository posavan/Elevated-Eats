using System.Collections.Generic;

namespace Capstone.Models
{
    public class Meal 
    {
        public int MealId { get; set; } = 0;
        public string MealName { get; set; }
        public string MealDescription { get; set; } = "";

        public List<Recipe> RecipeList { get; set; } = new List<Recipe>();

    }
}
