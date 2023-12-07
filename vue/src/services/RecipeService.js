import axios from "axios";

const http = axios.create({
  baseURL: "https://localhost:44315",
});

export default {
  list() {
    return http.get('/recipe');
  },
  listUserRecipes(userId) {
    return http.get('/recipe/' + userId);
  },
  createRecipe(newRecipe) {
    return http.post('/recipe', newRecipe);
  },
  addRecipeToUser(userId, newRecipe) {
    return http.post('/recipe/' + userId, newRecipe);
  },
  removeRecipeFromUser(userId, recipeId) {
    return http.delete('/recipe/'+userId +'/'+recipeId);
  },

  listIngredients(userId, recipeId) {
    return http.get('/recipe/' + userId + '/' + recipeId + '/ingredients');
  },
};
