<<<<<<< HEAD
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IRecipeDao
    {
        public IList<Recipe> GetRecipes();
        public IList<Recipe> GetRecipesByUserId(int userId);
        public Recipe GetRecipeById(int recipeId);
        public Recipe GetRecipeByName(string recipeName);
        public List<Ingredient> GetIngredientsByRecipeId(int recipeId);

        public int AddRecipeToUser(int userId, int recipeId);
        public Recipe CreateRecipe(Recipe recipe);
        public void AddIngredientsToRecipe(Recipe recipe);
      
        
    }
}

=======
﻿using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IRecipeDao
    {
        public IList<Recipe> GetRecipes();
        public IList<Recipe> GetRecipesByUserId(int userId);
        public Recipe GetRecipeById(int id);
        public Recipe GetRecipeByName(string name);
        public List<Ingredient> GetIngredientsByRecipeId(int recipeId);

        public int AddRecipeToUser(int userId, int recipeId);
        public Recipe CreateRecipe(Recipe recipe, int userId);
        public void AddIngredientsToRecipe(Recipe recipe);
      
        
    }
}
>>>>>>> 04454e7285a2ba0820efde32a474863c8f23b672
