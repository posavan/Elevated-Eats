<template>
  <div class="list-recipes">
    <h1>Favorite Recipes</h1>
    <section class="container">
      <recipe v-for="recipe in recipes" v-bind:key="recipe.recipeId" v-bind:item="recipe" />
      
    </section>
    <div class="add-recipe">
    <button v-show="!showForm" v-on:click="showForm = true">Add Recipe</button>
    </div>
    <form v-on:submit.prevent="createNewRecipe" v-show="showForm">
      <div>
        <label for="name">Name: </label>
        <input type="text" name="name" id="name" v-model="newRecipe.recipeName" />
      </div>
      <div>
        <label for="instructions">Instructions: </label>
        <input type="text" name="recipeInstructions" id="recipeInstructions" v-model="newRecipe.recipeInstructions" />
      </div>
      <div>
        <label for="image">Image URL: </label>
        <input type="text" name="recipeImage" id="recipeImage" v-model="newRecipe.recipeImage" />
      </div>
      <button type="submit">Save Recipe</button>
    </form>
  </div>
</template>

<script>
import recipe from "../components/Recipe.vue";
import recipeService from "../services/RecipeService.js";

export default {
  name: "ListRecipesView",
  components: { 
    recipe 
  },
  data() {
    return {
      recipes: [],
      showForm: false,
      newRecipe: {}
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
            location.reload();
            //this.$router.push({name:"favorites"})
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error adding recipe: ", error.response.status);
            } else if (error.request) {
              console.log("Error adding recipe: unable to communicate to server");
            } else {
              console.log("Error adding recipe: make request");
            }
          });
      }
    },
    loadRecipes() {
      recipeService
        .listUserRecipes()
        .then((response) => {
          console.log(response);
          this.recipes = response.data;
          this.$router.push('favorites');
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

  created() {
    this.loadRecipes();
  },
};
</script>


<style scoped>
section.recipe {

  font-family: Verdana, Geneva, Tahoma, sans-serif;
  border-radius: 1.25rem;
  margin-bottom: 1.25rem;
  width: 90%;
  padding: 2%;
  text-align: center;
  border-color: solid black;
  border-width: .2rem;
}
.container {
  position: center;
  flex-direction: row;
  text-align: center;
  padding: 10%;
  padding-top: .05%;
}

h1{
  text-align: center;
}
h4 {
  text-align: center;
}

form{
  text-align: center;
} 
div.label{
  font-weight: bold;
}
.add-recipe{
  display: block;
  color: white;
  text-align: center;
  padding: 10px;
  border-radius: 50px;
  margin-left: 50%;
  margin-right: 40%;
  border-top: none;
  border-left: none;
  border-right: none;
  margin-bottom: 0.9%;
  text-decoration: none;
  font-family: sans-serif;
  font-size: 16px;
}
</style>
