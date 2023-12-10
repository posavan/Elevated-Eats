using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IMealDao
    {
        public List<Meal> ListMeals();
       // public List<Meal> ListMealsById(int mealId);
        public Meal GetMeal(int MealId);

        public Meal AddAMeal(Meal newMeal);

        public Meal UpdateMeal(Meal newMeal);

        public bool DeleteAMeal(int mealId);

    }
}
