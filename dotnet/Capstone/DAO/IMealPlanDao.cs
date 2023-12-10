using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IMealPlanDao
    {
        public List<MealPlan> ListMeals();

        //public List<MealPlan> ListMealsById(int mealId);
        public MealPlan GetMealPlan(int MealPlanId);

        public MealPlan AddAMealPlan(MealPlan newMealPlan);
        public MealPlan AddMealToMealPlan(int mealId, MealPlan mealPlan);

        public MealPlan UpdateMealPlan(Meal newMealPlan);

        public bool DeleteAMealFromMealPlan(int mealId, int MealPlanId);

        public bool DeleteAMealPlan(int mealPlanId);

    }
}
