using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IMealPlanDao
    {
        public List<MealPlan> ListMeals();
        public List<MealPlan> ListMealsById(int mealId);
        public MealPlan GetMealPlan(int MealPlanId);

        public MealPlan AddAMealPlan(Meal newMeal);

        public MealPlan UpdateMealPlan(Meal newMeal);

        public bool DeleteAMealPlan(int mealId);

    }
}
