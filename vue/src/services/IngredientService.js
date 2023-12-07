import axios from "axios";


export default {
  list() {
    return axios.get('/ingredients');
  },

  createIngredient(newIngredient) {
    return axios.post(`/ingredients`, newIngredient)
  }
};
