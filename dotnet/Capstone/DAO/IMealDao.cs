using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IMealDao
    {
        public List<Meal> ListMeals();

       // public List<Meal> ListMealsById(int mealId);
        public Meal GetMeal(int MealId);

        public Meal CreateMeal(Meal newMeal);

        public Meal UpdateMeal(Meal updatedMeal);

        public bool DeleteMeal(int mealId);

    }
}
