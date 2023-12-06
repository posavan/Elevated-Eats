import axios from "axios";

const http = axios.create({
  baseURL: "https://localhost:44315",
});

export default {
  list() {
    return http.get('/recipe');
  },

  listUserRecipes(userId) {
    return http.get('/recipe/' + userId );
  },
  

  listIngredients(recipeId , userId){
    return http.get('/recipe/' + recipeId + '/'+ userId + '/ingredients')
  },


  createRecipe(newRecipe) {
    return http.post(`/recipe`, newRecipe)
  }
};
