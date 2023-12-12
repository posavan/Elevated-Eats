import axios from "axios";

export default {
    list() {
        return axios.get('/mealplan');
    },
    listMealsFromPlan(mealPlanId) {
        return axios.get(`/mealplan/meals/${mealPlanId}`);
    },
    getMealPlan(mealPlanId) {
        return axios.get(`/mealplan/${mealPlanId}`);
    },
    getGroceries(mealPlanId) {
        return axios.get(`/mealplan/${mealPlanId}/groceries`);
    },

    createMealPlan(newMealPlan) {
        return axios.post(`/mealplan`, newMealPlan);
    },
    updateMealPlan(newMealPlan) {
        return axios.put(`/mealplan`, newMealPlan);
    },
    addMealToPlan(mealPlanId, meal) {
        return axios.post(`/mealplan/${mealPlanId}`, meal);
    },
    removeMealFromPlan(mealPlanId, mealId) {
        return axios.delete(`/mealplan/${mealPlanId}/${mealId}`);
    },
    deleteMealPlan(mealPlanId) {
        return axios.delete(`/mealplan/${mealPlanId}`);
    }

};
