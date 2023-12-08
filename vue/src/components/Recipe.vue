<template>
  <section class="recipe">
    <h3>Name: {{ item.recipeName }}</h3>
    <h4>Ingredients:</h4>
    <section class="container">
      <ingredient v-for="ingredient in item.ingredientList" v-bind:key="ingredient.id" v-bind:item="ingredient" />
    </section>
    <p>Instructions: {{ item.recipeInstructions }}</p>
    <div class="button-container">
      <button class="save-recipe" v-on:click.prevent="saveRecipe" v-if="!item.favorite">Save Recipe To
        Favorites</button>
      <button class="remove-recipe" v-on:click.prevent="removeRecipe" v-if="item.favorite">Remove Recipe From
        Favorites</button>
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
  methods: {
    loadRecipeIngredients(recipeId) {
      recipeService
        .listIngredients(this.$store.state.user.userId, recipeId)
        .then((response) => {
          console.log("Reached created in ListIngredientsView.vue");
          console.log(response.data);
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
      console.log("In recipe vie, saveRecipe: ", this.item);
      let savedRecipe = Object.assign({ favorite: true }, this.item);
      this.$store.commit('SAVE_RECIPE', savedRecipe);
      // recipeService
      // console.log("reached recipeService")
      //   .addRecipeToUser(this.userId, this.item)
      //   .then((response) => {
      //     console.log("Reached SAVE_RECIPE in Vuex");
      //     console.log(response);
      //     //@todo: refresh state.recipes
      //   })
      //   .catch((error) => {
      //     if (error.response) {
      //       console.log("Error loading recipes: ", error.response.status);
      //     } else if (error.request) {
      //       console.log(
      //         "Error loading recipes: unable to communicate to server"
      //       );
      //     } else {
      //       console.log("Error loading recipes: make request");
      //     }
      //   });
    },

    removeRecipe(item) {
      let savedRecipe = Object.assign({ favorite: false }, item);
      this.$store.commit('REMOVE_RECIPE', savedRecipe);
    },
    
  },

  created() {
    console.log("Reached created in Recipe.vue");
    let id = this.item.recipeId;
    this.loadRecipeIngredients(id);
  },
};
</script>

<style scoped>
.list-all-recipes {
    font-family: Verdana, Geneva, Tahoma, sans-serif;
    text-align: center; /* Center text for better appearance */
  }

  h1 {
    text-align: center;
  }

  .container {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
    gap: 1.25rem;
  }

  recipe {
    background-color: pink;
    border-radius: 0.625rem; /* Rounded corners for recipe cards */
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
    margin-top: 1.25rem; /* Add spacing between the button and the form */
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

</style>
