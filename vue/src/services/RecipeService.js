import axios from "axios";
import RecipeDetailsViewVue from "../views/RecipeDetailsView.vue";

export default {
  list() {
    return axios.get('/recipe');
  },
  listUserRecipes() {
    return axios.get('/recipe/favorites');
  },
  GetUserRecipeByRecipeId(recipeId) {
    return axios.get('/recipe/favorites/' + recipeId);
  },
  listRecipeByName(recipeName) {
    return axios.get('/recipe/public/' + recipeName);
  },

  createRecipe(newRecipe) {
    return axios.post('/recipe', newRecipe);
  },
  addRecipeToUser(newRecipe) {
    return axios.post('/recipe/favorites', newRecipe);
  },
  removeRecipeFromUser(recipeId) {
    return axios.delete('/recipe/favorites/' + recipeId);
  },

  listIngredients(recipeId) {
    return axios.get('/recipe/favorites/' + recipeId + '/ingredients');
  },
  updateRecipe(recipeId, newRecipe) {
    return axios.put('/recipe/favorites/' + recipeId, newRecipe);
  },
  addIngredientToRecipe(recipeId, newRecipe) {
    return axios.put('/recipe/favorites/' + recipeId + '/ingredients', newRecipe);
  },
  removeIngredientFromRecipe(recipeId, ingredientId) {
    return axios.delete('/recipe/favorites/' + recipeId + '/ingredients/' + ingredientId);
  },


};
