<template>
    <div class="list-all-recipes">
      <h1>All Recipes</h1>
      <section class="container">
        <recipe
          v-for="recipe in recipes"
          v-bind:key="recipe.id"
          v-bind:item="recipe"
        >
        <button v-on:click="addRecipeToUser">Save Recipe</button>
    </recipe>

      </section>
  
      <button v-show="!showForm" v-on:click="showForm = true">Add Recipe</button>
  
      <form v-on:submit.prevent="createNewRecipe" v-show="showForm">
        <div>
          <label for="name">Name: </label>
          <input
            type="text"
            name="name"
            id="name"
            v-model="newRecipe.recipeName"
          />
        </div>
        <div>
          <label for="type">Description: </label>
          <input
            type="text"
            name="recipeDescription"
            id="recipeDescription"
            v-model="newRecipe.recipeDescription"
          />
        </div>
        <!-- 
        <div>
          <h2>Ingredients: </h2>
          <label for="type">Quantity: </label>
          <input type="text" name="quantity" id="quantity" v-model="newIngredient.quantity" />
          <label for="type">Ingredient: </label>
          <input type="text" name="name" id="name" v-model="newIngredient.ingredientName" />
          <label for="type">Calories: </label>
          <input type="text" name="calories" id="calories" v-model="newIngredient.calories" />
          addIngredientToRecipe
          <recipe v-for="ingredient in newRecipe.ingredientList" v-bind:key="ingredient.id"
            v-bind:ingredient="ingredient" />
          
            <button class="btn-add" v-on:click="$router.push({ name: 'AddIngredientView', params: { recipeId: newRecipe.id } })">Add
            Ingredient</button>
             
        </div> -->
        <button type="submit">Save Recipe</button>
      </form>
    </div>
  </template>
  
  <script>
  import recipe from "../components/Recipe.vue";
  import recipeService from "../services/RecipeService.js";
  //import ListIngredientsView from "./ListIngredientsView.vue";
  
  export default {
    components: { recipe }, //ListIngredientsView
    name: "ListAllRecipesView",
    data() {
      return {
        recipes: [],
        showForm: false,
        newRecipe: {},
        newIngredient: {},
        userId: 0,
      };
    },

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
      addIngredientToRecipe() {
        this.newRecipe.ingredientList.add(this.newIngredient);
        this.newIngredient = {};
      },
    },
  
    created() {

      this.loadRecipes();
    },
  };
  </script>
  
  
  <style scoped>
  section.recipe {
    background-color: pink;
    font-family: Verdana, Geneva, Tahoma, sans-serif;
  }
  </style>
  