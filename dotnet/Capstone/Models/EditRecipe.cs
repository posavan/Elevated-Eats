using System.Collections.Generic;

namespace Capstone.Models
{
    public class EditRecipe
    {
        public int RecipeId { get; set; }
        public List<Ingredient> IngredientList { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public object RecipeName { get; set; }
        public string RecipeInstructions { get; set; }
    }
}
