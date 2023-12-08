using System.Collections.Generic;

namespace Capstone.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public List<Ingredient> IngredientList { get; set; }
        public string RecipeInstructions { get; set; }

    }
}
