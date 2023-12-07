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

        public Recipe AddRecipeToUser(Recipe recipe, int userId);
        public Recipe CreateRecipe(Recipe recipe, int userId);
        public void AddIngredientsToRecipe(Recipe recipe);
      
        
    }
}
