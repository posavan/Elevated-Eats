import axios from "axios";

export default {
  list() {
    return axios.get('/recipe');
  },
  listUserRecipes(userId) {
    return axios.get('/recipe/' + userId);
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
