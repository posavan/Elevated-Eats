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

        public bool AddMealToMealPlan(int mealPlanId, int mealId);
        public bool RemoveMealFromMealPlan(int mealPlanId, int mealId);
        public bool DeleteMealPlan(int mealPlanId);
        
        public List<Meal> GetMealsByMealPlanId(int mealPlanId);

    }
}
