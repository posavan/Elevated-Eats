<template>
    <form v-on:submit.prevent="createNewRecipe">
      <div>
        <label for="name">Name: </label>
        <input type="text" name="name" id="name" v-model="editRecipe.recipeName" />
      </div>
      <div>
        <label for="instructions">Instructions: </label>
        <input type="text" name="instructions" id="instructions" v-model="editRecipe.recipeInstructions"/>
      </div>
      <div>
        <label for="image">Image URL: </label>
        <input type="text" name="image" id="image" v-model="editRecipe.recipeImage"/>
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
          recipeInstructions: this.recipe.recipeInstructions,
          recipeImage: this.recipe.recipeImage,
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
  