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
              console.log("Error loading ingredients: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error loading ingredients: unable to communicate to server"
              );
            } else {
              console.log("Error loading ingredients: make request");
            }
          });
      },

      ADD_NEW_INGREDIENT(state, payload) {
        state.ingredients.push(payload);//@todo: refactor
      },

      LOAD_ALL_RECIPES(state) {
        recipeService
          .list()
          .then((response) => {
            console.log("Reached LOAD_ALL_RECIPES in Vuex");
            console.log(response);
            state.allRecipes = response.data;
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error loading recipes: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error loading recipes: unable to communicate to server"
              );
            } else {
              console.log("Error loading recipes: make request");
            }
          });
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
              console.log("Error loading recipes: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error loading recipes: unable to communicate to server"
              );
            } else {
              console.log("Error loading recipes: make request");
            }
          });
      },

      ADD_RECIPE(state, payload) {
        recipeService
          .createRecipe(payload)
          .then((response) => {
            console.log("Reached ADD_RECIPE in Vuex");
            console.log(response);
            //@todo: refresh state.recipes?
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error loading recipes: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error loading recipes: unable to communicate to server"
              );
            } else {
              console.log("Error loading recipes: make request");
            }
          });
      },

      SAVE_RECIPE(state, payload) {
        console.log("In save recipe: ", payload);
        recipeService
          .addRecipeToUser(state.user.userId, payload)
          .then((response) => {
            console.log("Reached SAVE_RECIPE in Vuex");
            console.log(response);
            //@todo: refresh state.recipes
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error loading recipes: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error loading recipes: unable to communicate to server"
              );
            } else {
              console.log("Error loading recipes: make request");
            }
          });
      },

      REMOVE_RECIPE(state, payload) {
        recipeService
          .removeRecipeFromUser(state.user.userId, payload)
          .then((response) => {
            console.log("Reached SAVE_RECIPE in Vuex");
            console.log(response);
            //@todo: refresh state.recipes
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error loading recipes: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error loading recipes: unable to communicate to server"
              );
            } else {
              console.log("Error loading recipes: make request");
            }
          });
      },
    },
  });
  return store;
}
