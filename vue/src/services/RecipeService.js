import axios from "axios";
import RecipeDetailsViewVue from "../views/RecipeDetailsView.vue";

export default {
  list() {
    return axios.get('/recipe');
  },
  listUserRecipes(userId) {
    return axios.get('/recipe/' + userId);
  },
  GetUserRecipeByRecipeId(recipeId, userId) {
    return axios.get('recipe/' + userId + '/' + recipeId);
  },
  listRecipeByName(recipeName) {
    return axios.get('/recipe/public/' + recipeName);
  },

  createRecipe(newRecipe) {
    return axios.post('/recipe', newRecipe);
  },
  addRecipeToUser(userId, newRecipe) {
    return axios.post('/recipe/' + userId, newRecipe);
  },
  removeRecipeFromUser(userId, recipeId) {
    return axios.delete('/recipe/' + userId + '/' + recipeId);
  },

  listIngredients(userId, recipeId) {
    return axios.get('/recipe/' + userId + '/' + recipeId + '/ingredients');
  },
  updateRecipe(userId, recipeId, newRecipe) {
    return axios.put('/recipe/' + userId + '/' + recipeId, newRecipe);
  },
  addIngredientToRecipe(userId, recipeId, newRecipe) {
    return axios.put('/recipe/' + userId + '/' + recipeId + '/ingredients', newRecipe);
  },
  removeIngredientFromRecipe(userId, recipeId, ingredientId) {
    return axios.delete('/recipe/' + userId + '/' + recipeId + '/ingredients', ingredientId);
  },


};
