<template>
    <form v-on:submit.prevent="createNewRecipe">
      <div>
        <label for="name">Name: </label>
        <input type="text" name="name" id="name" v-model="editRecipe.recipeName" />
      </div>
      <div>
        <label for="description">Description: </label>
        <input type="text" name="description" id="description" v-model="editRecipe.recipeDescription" />
      </div>
      <div class="actions">
        <button class="btn-submit" type="submit">Add and Continue</button>
        <button class="btn-cancel" type="button" v-on:click="cancelForm">Return</button>
      </div>
    </form>
  </template>
  
  <script>
  import recipeService from "../services/RecipeService.js";
  
  export default {
    props: {
      recipe: {
        type: Object,
        required: true,
      },
    },
    data() {
      return {
        editRecipe: {
          recipeId: this.recipe.recipeId,
          recipeName: this.recipe.recipeName,
          recipeDescription: this.recipe.recipeDescription,
        },
        mealId: this.recipe.mealId,
      };
    },
  
    methods: {
      createNewRecipe() {
        if (this.editRecipe.recipeName) {
          recipeService
            .createRecipe(this.editRecipe)
            .then(() => {
              this.editRecipe = {};
              this.$router.push({ name: "favorites" });
            })
            .catch((error) => {
              if (error.response) {
                console.log("Error adding recipe: ", error.response.status);
              } else if (error.request) {
                console.log(
                  "Error adding recipe: unable to communicate to server"
                );
              } else {
                console.log("Error adding recipe: make request");
              }
            });
        }
      },
      cancelForm() {
        this.$router.back();
      },
    },
  };
  </script>
  