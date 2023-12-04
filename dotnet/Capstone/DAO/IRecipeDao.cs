using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IRecipeDao
    {
        public IList<Recipe> GetRecipes();

        public Recipe GetRecipeById(int id);
        public Recipe GetRecipeByName(string name);
        public List<Ingredient> GetIngredientsByRecipeName(string recipeName);

        public Recipe CreateRecipe(Recipe recipe);
        public void AddIngredientsToRecipe(Recipe recipe);

    }
}
