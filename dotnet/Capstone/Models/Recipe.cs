using System.Collections.Generic;

namespace Capstone.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string RecipeName { get; set; }
        public string RecipeInstructions { get; set; } = "";
        public List<Ingredient> IngredientList { get; set; } = new List<Ingredient>();
    }
}
