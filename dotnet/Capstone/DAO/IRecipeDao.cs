<<<<<<< HEAD
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
        public Recipe ModifyRecipe(Recipe updatedRecipe);
        public void AddIngredientsToRecipe(Recipe recipe);
        public void RemoveIngredientsFromRecipe(int userRecipeId, int ingredientId);



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
        public Recipe GetUserRecipeById(int recipeId);
        public Recipe GetRecipeById(int id);
        public Recipe GetRecipeByName(string name);
        public List<Ingredient> GetIngredientsByRecipeId(int recipeId);

        public int AddRecipeToUser(int userId, int recipeId);
        public Recipe CreateRecipe(Recipe recipe, int userId);
        public void AddIngredientsToRecipe(Recipe recipe);
      
        
    }
}
>>>>>>> b434f0d9a2d3e98b1ed007d7c33a9b252e524d7c
