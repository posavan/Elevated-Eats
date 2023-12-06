import { createStore as _createStore } from "vuex";
import axios from "axios";

import ingredientService from "../services/IngredientService";
import recipeService from "../services/RecipeService";

export function createStore(currentToken, currentUser) {
  let store = _createStore({
    state: {
      token: currentToken || "",
      user: currentUser || {},

      ingredients: [],
    },

    mutations: {
      SET_AUTH_TOKEN(state, token) {
        state.token = token;
        localStorage.setItem("token", token);
        axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      },
      SET_USER(state, user) {
        state.user = user;
        localStorage.setItem("user", JSON.stringify(user));
      },

      LOGOUT(state) {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        state.token = "";
        state.user = {};
        axios.defaults.headers.common = {};
      },

      LOAD_INGREDIENTS(state) {
        ingredientService
          .list()
          .then((response) => {
            console.log("Reached LOAD_INGREDIENTS in Vuex");
            console.log(response);
            state.ingredients = response.data;
          })
          .catch((error) => {
            if (error.response) {
              // error.response exists
              // Request was made, but response has error status (4xx or 5xx)
              console.log("Error loading ingredients: ", error.response.status);
            } else if (error.request) {
              // There is no error.response, but error.request exists
              // Request was made, but no response was received
              console.log(
                "Error loading ingredients: unable to communicate to server"
              );
            } else {
              // Neither error.response and error.request exist
              // Request was *not* made
              console.log("Error loading ingredients: make request");
            }
          });
      },

      ADD_INGREDIENT(state, payload) {
        state.ingredients.push(payload);
      },

      LOAD_RECIPES(state) {
        recipeService
          .listUserRecipes(state.user.userId)
          .then((response) => {
            console.log("Reached LOAD_RECIPES in Vuex");
            console.log(response);
            state.recipes = response.data;
          })
          .catch((error) => {
            if (error.response) {
              // error.response exists
              // Request was made, but response has error status (4xx or 5xx)
              console.log("Error loading recipes: ", error.response.status);
            } else if (error.request) {
              // There is no error.response, but error.request exists
              // Request was made, but no response was received
              console.log(
                "Error loading recipes: unable to communicate to server"
              );
            } else {
              // Neither error.response and error.request exist
              // Request was *not* made
              console.log("Error loading recipes: make request");
            }
          });
      },

      ADD_RECIPE(state, payload) {
        state.recipes.push(payload);
      },
    },
  });
  return store;
}
