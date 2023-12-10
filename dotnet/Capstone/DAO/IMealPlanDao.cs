using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IMealPlanDao
    {
        public List<MealPlan> ListMealPlansByUserId(int userId);
        public MealPlan GetMealPlanById(int mealPlanId);

        public MealPlan CreateMealPlan(MealPlan mealPlan, int userId);
        public MealPlan UpdateMealPlan(MealPlan mealPlan, int userId);

        public bool AddMealToMealPlan(int mealId, int mealPlanId);
        public bool RemoveMealFromMealPlan(int mealId, int mealPlanId);
        public bool DeleteMealPlan(int mealPlanId);

    }
}
