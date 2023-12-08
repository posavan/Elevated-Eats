<template>
  <section class="recipe">
    <h3>Name: {{ recipe.recipeName }}</h3>
    <h4>Ingredients:</h4>
    <section class="container">
      <ingredient v-for="ingredient in recipe.ingredientList" v-bind:key="ingredient.id" v-bind:item="ingredient" />
    </section>
    <p>Instructions: {{ recipe.recipeInstructions }}</p>
    <div class="button-container">
      <button class="save-recipe" v-on:click.prevent="saveRecipe" v-if="this.$route.name == 'recipe'">Add Recipe To
        Favorites</button>
      <button class="remove-recipe" v-on:click.prevent="removeRecipe" v-if="this.$route.name == 'userRecipe'">Remove
        Recipe From Favorites</button>
    </div>
    <p></p>
  </section>
</template>

<script>
import Ingredient from "../components/Ingredient.vue";
import recipeService from "../services/RecipeService.js";

export default {
  name: "recipe",
  props: ["item"],
  components: {
    Ingredient
  },
  data() {
    return {
      recipe: {},
    }
  },
  methods: {
    loadRecipeIngredients() {
      console.log("reached loadRecipeIngrd");
      recipeService
        .listIngredients(this.$store.state.user.userId, this.recipe.recipeId)
        .then((response) => {
          this.ingredients = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading recipe ingredients: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error loading ingredients: unable to communicate to server"
            );
          } else {
            console.log("Error loading ingredients: make request");
          }
        });
    },

    saveRecipe() {
      this.recipe.favorite = true;
      recipeService
        .addRecipeToUser(this.$store.state.user.userId, this.recipe)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error saving recipe: ", error.response.status);
          } else if (error.request) {
            console.log("Error saving recipe: unable to communicate to server");
          } else {
            console.log("Error saving recipe: make request");
          }
        });

    },

    removeRecipe() {
      this.recipe.favorite = false;
      console.log("reached removeRecipe", this.recipe);
      recipeService
        .removeRecipeFromUser(this.$store.state.user.userId, this.recipe.recipeId)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error removing recipe: ", error.response.status);
          } else if (error.request) {
            console.log("Error removing recipe: unable to communicate to server");
          } else {
            console.log("Error removing recipe: make request");
          }
        });
    },

  },

  created() {
    this.recipe = this.item;
    this.loadRecipeIngredients();
  },
};
</script>

<style scoped>
.list-all-recipes {
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  text-align: center;
  /* Center text for better appearance */
  padding-right: 20%;
}


h1 {
  text-align: center;
}

.container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
  gap: 1.25rem;
}

.recipe {
  background-color: rgb(225, 203, 164);
  border-radius: 0.625rem;
  /* Rounded corners for recipe cards */
  box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.1);
  margin-bottom: 1.25rem;
  padding: 2%;
}

recipe {
  background-color: pink;
  border-radius: 0.625rem;
  /* Rounded corners for recipe cards */
  box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.1);
  margin-bottom: 1.25rem;
  padding: 2%;
}

.add-recipe-button {
  margin-top: 1.25rem;
  align-items: center;
}

form {
  display: flex;
  flex-direction: column;
  max-width: 18.75rem;
  margin: auto;
  margin-top: 1.25rem;
  /* Add spacing between the button and the form */
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
  background-color: brown;
  color: #fff;
  border: none;
  border-radius: 0.25rem;
  cursor: pointer;
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
}</style>
