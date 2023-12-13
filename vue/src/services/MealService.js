import axios from "axios";

export default {
    list() {
        return axios.get('/meal');
    },
    getMeal(mealId) {
        return axios.get(`/meal/${mealId}`);
    },
    createMeal(newMeal) {
        return axios.post(`/meal`, newMeal);
    },
    listRecipesFromMeal(mealId) {
        return axios.get(`/meal/${mealId}/recipes`);
    },
    updateMeal(newMeal) {
        return axios.put(`/meal`, newMeal);
    },
    addRecipeToMeal(mealId, recipe) {
        return axios.post(`/meal/${mealId}`, recipe);
    },
    removeRecipeFromMeal(mealId, recipeId) {
        return axios.delete(`/meal/${mealId}/${recipeId}`);
    },
    deleteMeal(mealId) {
        return axios.delete(`/meal/${mealId}`);
    }

};