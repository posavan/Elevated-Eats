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
        .listIngredients(this.userId, recipeId)
        .then((response) => {
          console.log("Reached created in ListIngredientsView.vue");
          console.log(response);
          this.ingredients = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading ingredients: ", error.response.status);
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
    }
  },

  created() {
    console.log("Reached created in Recipe.vue");
    console.log(this.item);
    console.log(this.item.recipeId);
    let id = this.item.recipeId;
    this.loadRecipeIngredients(id);
  },
};
</script>
