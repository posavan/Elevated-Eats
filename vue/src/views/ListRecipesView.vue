<template>
    <div class="list-recipes">
      <h1>Recipes</h1>
      <section class="container">
        <recipe v-for="recipe in recipes" v-bind:key="recipe.id" v-bind:item="recipe" />
      </section>
  
      <button v-show="!showForm" v-on:click="showForm = true">
        Add Recipe
      </button>
  
      <form v-on:submit.prevent="createNewRecipe" v-show="showForm">
        <div>
          <label for="name">Name: </label>
          <input type="text" name="name" id="name" v-model="newRecipe.recipeName" />
        </div>
        <div>
          <label for="type">Description: </label>
          <input type="text" name="recipeDescription" id="recipeDescription" v-model="newRecipe.recipeDescription" />
        </div>
        <div>
          <label for="type">Ingredients: </label><!--TODO: allow multiple entries etc...-->
          <input type="text" name="quantity" id="quantity" >
          <input type="text" name="ingredient" id="ingredient" v-model="newRecipe.ingredientList" />
        </div>
        <button type="submit">Save Recipe</button>
      </form>
    </div>
  </template>
    
  <script>
  import recipe from "../components/Recipe.vue";
  import recipeService from "../services/RecipeService.js";
  
  export default {
    components: { recipe },
    name: "ListRecipesView",
    data() {
      return {
        recipes: [],
        showForm: false,
        newRecipe: {},
      };
    },
    // computed: {
    //   currentRecipes() {
    //     return this.$store.state.recipes;
    //   },
    // },
    methods: {
      createNewRecipe() {
        if (this.newRecipe.recipeName) {
          recipeService
            .createRecipe(this.newRecipe)
            .then(() => {
              this.newRecipe = {};
              this.showForm = false;
              this.loadRecipes();
            })
            .catch((error) => {
              if (error.response) {
                // error.response exists
                // Request was made, but response has error status (4xx or 5xx)
                console.log("Error adding recipe: ", error.response.status);
              } else if (error.request) {
                // There is no error.response, but error.request exists
                // Request was made, but no response was received
                console.log(
                  "Error adding recipe: unable to communicate to server"
                );
              } else {
                // Neither error.response and error.request exist
                // Request was *not* made
                console.log("Error adding recipe: make request");
              }
            });
        }
      },
  
      loadRecipes() {
        recipeService
          .list()
          .then((response) => {
            console.log("Reached created in ListRecipesView.vue");
            console.log(response);
            this.recipes = response.data;
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
      }
    },
  
    created() {
      this.loadRecipes();
    },
  };
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  
  section.recipe{
    background-color: rgb(19, 218, 148);
    font-family: Verdana, Geneva, Tahoma, sans-serif;
  }
  </style>
  