import axios from "axios";

export default {
  list() {
    return axios.get('/recipe');
  },
  listUserRecipes(userId) {
    return axios.get('/recipe/' + userId);
  },
  GetUserRecipeByRecipeId(recipeId, userId) {
    return http.get('recipe/'+ userId +'/'+ recipeId);
  },
  listRecipeByName(recipeName) {
    return http.get('/recipe/public/' + recipeName);
  },
  createRecipe(newRecipe) {
    return axios.post('/recipe', newRecipe);
  },
  addRecipeToUser(userId, newRecipe) {
    console.log(newRecipe);
    return axios.post('/recipe/' + userId, newRecipe);
  },
  removeRecipeFromUser(userId, recipeId) {
    return axios.delete('/recipe/' + userId + '/' + recipeId);
  },

  listIngredients(userId, recipeId) {
    return axios.get('/recipe/' + userId + '/' + recipeId + '/ingredients');
  },

};
