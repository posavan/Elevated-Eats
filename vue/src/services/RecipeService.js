import axios from "axios";

const http = axios.create({
  baseURL: "https://localhost:44315",
});

export default {
  list() {
    return http.get('/recipe');
  },

  createRecipe(newRecipe) {
    return http.post(`/recipe`, newRecipe)
  }
};
