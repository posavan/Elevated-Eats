<template>
  <div class="list-recipes">
    <h1>Favorite Recipes</h1>
    <section class="container">
      <recipe v-for="recipe in recipes" v-bind:key="recipe.id" v-bind:item="recipe" />
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
        <label for="type">Instructions: </label>
        <input type="text" name="recipeInstructions" id="recipeInstructions" v-model="newRecipe.recipeInstructions" />
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
  name: "ListRecipesView",
  data() {
    return {
      recipes: [],
      showForm: false,
      newRecipe: {},
      newIngredient: {},
      userId: 0,
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
      console.log(this.userId)
      recipeService
        .listUserRecipes(this.userId)
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
    this.userId = this.$route.params.userId;
    this.loadRecipes();
  },
};
</script>


<style scoped>
section.recipe {
  background-color: rgb(214, 195, 157);
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  background-color: rgb(224, 203, 163);
  border-radius: 0.5rem;
  margin-bottom: 1.25rem;
  width: 90%;
  padding: 2%;
}
.container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
  gap: 10px;
}
h1{
  text-align: center;
}
form{
  text-align: center;
} 

.add-recipe{
  display: block;
  color: wheat;
  text-align: center;
  padding: 14px 16px;
  background-color: rgb(213, 51, 51);
  border-radius: 50px;
  margin-left: 40%;
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
