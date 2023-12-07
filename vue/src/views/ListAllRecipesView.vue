<template>
    <div class="list-all-recipes">
      <h1>All Recipes</h1>
      <section class="container">
        <recipe
          v-for="recipe in allRecipes"
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
        <label for="type">Instructions: </label>
        <input
          type="text"
          name="recipeInstructions"
          id="recipeInstructions"
          v-model="newRecipe.recipeInstructions"
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
        allRecipes: [],
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
            console.log("Reached created in ListAllRecipesView.vue");
            console.log(response);
            this.allRecipes = response.data;
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

  .list-all-recipes {
    font-family: Verdana, Geneva, Tahoma, sans-serif;
  }

  h1 {
    text-align: center;
  }

  .container {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
    gap: 1.25rem;
  }

  form {
    display: flex;
    flex-direction: column;
    max-width: 18.75rem;
    margin: auto;
  }

  form div {
    margin-bottom: 0.625rem;
  }

  label {
    display: block;
    margin-bottom: 0.3125rem;
  }

  input {
    width: 100%;
    padding: 0.625rem;
    box-sizing: border-box;
  }

  button {
    padding: 1.25rem;
    background-color: #4caf50;
    color: #fff;
    border: none;
    border-radius: 0.25rem;
    cursor: pointer;
  }

  button:hover {
    opacity: 0.8;
  }

  .container recipe {
    background-color: pink;
    border-radius: 0.5rem;
    margin-bottom: 1.25rem;
    width: 30%;
    padding: 2%;
  }
  </style>
  