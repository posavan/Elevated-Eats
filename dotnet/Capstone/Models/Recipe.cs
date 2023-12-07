using System.Collections.Generic;

namespace Capstone.Models
{
    public class Recipe
    {
        public string RecipeName { get; set; }
        public int RecipeId { get; set; }
        public List<Ingredient> IngredientList { get; set; }
        public string RecipeInstructions { get; set; }
        public bool Saved { get; set; } = false;

    }
}
