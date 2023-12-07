import axios from "axios";

const http = axios.create({
  baseURL: "https://localhost:44315",
});

export default {
  list() {
    return http.get('/recipe');
  },
<<<<<<< HEAD
  
=======
>>>>>>> 04454e7285a2ba0820efde32a474863c8f23b672
  listUserRecipes(userId) {
    return http.get('/recipe/' + userId);
  },
<<<<<<< HEAD
  
  listRecipeByName(recipeName) {
    return http.get('/recipe/public' + recipeName );
  },

  listIngredients(recipeId , userId){
    return http.get('/recipe/' + recipeId + '/'+ userId + '/ingredients')
  },


  createRecipe(newRecipe) {
    return http.post(`/recipe`, newRecipe)
  }

=======
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
>>>>>>> 04454e7285a2ba0820efde32a474863c8f23b672
};
