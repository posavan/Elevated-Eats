using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IMealDao
    {
        public List<Meal> ListMeals();
        public Meal GetMeal(int MealId);
        public Meal CreateMeal(Meal newMeal);
        public Meal UpdateMeal(Meal updatedMeal);
        public bool AddRecipeToMeal(int mealId, Recipe recipe);
        public bool RemoveRecipeFromMeal(int mealId, int recipeId);

        public bool DeleteMeal(int mealId);

        public List<Recipe> GetRecipesByMealId(int mealId);

    }
}
