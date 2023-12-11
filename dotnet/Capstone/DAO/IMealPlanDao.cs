using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IMealPlanDao
    {
        public List<MealPlan> ListMeals();
        public List<MealPlan> ListMealsById(int mealId);
        public MealPlan GetMealPlan(int MealPlanId);

        public MealPlan CreateMealPlan(Meal newMeal);

        public MealPlan UpdateMealPlan(Meal newMeal);

        public bool AddMealToMealPlan(int mealPlanId, int mealId);
        public bool RemoveMealFromMealPlan(int mealPlanId, int mealId);
        public bool DeleteMealPlan(int mealPlanId);

    }
}
