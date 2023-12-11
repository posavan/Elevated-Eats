import axios from "axios";

export default {
    list() {
        return axios.get('/mealplan');
    },
    getMealPlan(mealPlanId) {
        return axios.get(`/mealplan/${mealPlanId}`);
    },
    createMealPlan(newMealPlan) {
        return axios.post(`/mealplan`, newMealPlan);
    },
    updateMealPlan(newMealPlan) {
        return axios.put(`/mealplan/`, newMealPlan);
    },
    addMealToPlan(mealPlanId, mealId) {
        return axios.post(`/mealplan/${mealPlanId}`, mealId);
    },
    removeMealFromPlan(mealPlanId, mealId) {
        return axios.delete(`/mealplan/${mealPlanId}/${mealId}`);
    },
    deleteMealPlan(mealPlanId) {
        return axios.delete(`/mealplan/${mealPlanId}`);
    }

};
