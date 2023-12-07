<template>
    <div class="list-all-recipes">
      <h1>Recipe Library</h1>
      <section class="container">
        <recipe
          v-for="recipe in allRecipes"
          v-bind:key="recipe.id"
          v-bind:item="recipe"
        >
        <button v-on:click="addRecipeToUser">Save Recipe To Favorites</button>
    </recipe>

      </section>
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
        <div>
        <label for="type">Instructions: </label>
        <input
          type="text"
          name="recipeInstructions"
          id="recipeInstructions"
          v-model="newRecipe.recipeInstructions"
        />
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
  </style>
  