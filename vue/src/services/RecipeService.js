import axios from "axios";
import RecipeDetailsViewVue from "../views/RecipeDetailsView.vue";

export default {
  list() {
    return axios.get('/recipe');
  },
  createRecipe(newRecipe) {
    return axios.post('/recipe', newRecipe);
  },
  listRecipeByName(recipeName) {
    return axios.get('/recipe/public/' + recipeName);
  },
  listUserRecipes() {
    return axios.get('/recipe/favorites');
  },
  GetUserRecipeByRecipeId(recipeId) {
    return axios.get('/recipe/favorites/' + recipeId);
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

  updateRecipe(newRecipe) {
    return axios.put('/recipe/favorites/edit', newRecipe);
  },
  addIngredientsToRecipe(newRecipe) {
    return axios.put('/recipe/favorites/edit/ingredients', newRecipe);
  },
  removeIngredientFromRecipe(recipeId, ingredientId) {
    return axios.delete('/recipe/favorites/' + recipeId + '/ingredients/' + ingredientId);
  },


};
