import axios from "axios";

export default {
    list() {
        return axios.get('/meal');
    },
    // getMeal(mealId) {
    //     return axios.get(`/meal/${mealId}`);
    // },
    createMeal(newMeal) {
        return axios.post(`/meal`, newMeal);
    },
    listRecipesFromMeal(mealId) {
        return axios.get(`/meal/${mealId}`);
    },
    updateMeal(newMeal) {
        return axios.put(`/meal`, newMeal);
    },
    addRecipeToMeal(mealId, recipeId) {
        return axios.post(`/meal/${mealId}`, recipeId);
    },
    removeRecipeFromMeal(mealId, recipeId) {
        return axios.delete(`/meal/${mealId}/${recipeId}`);
    },
    deleteMeal(mealId) {
        return axios.delete(`/meal/${mealId}`);
    }

};