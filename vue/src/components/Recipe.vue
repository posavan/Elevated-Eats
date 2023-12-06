<template>
  <section class="recipe">
    <p>Name: {{ item.recipeName }}</p>
    <p>Description: {{ item.recipeDescription }}</p>
    <p>Ingredients:</p>
    <section class="container">
      <ingredient
        v-for="ingredient in item.ingredientList"
        v-bind:key="ingredient.id"
        v-bind:item="ingredient"
      />
    </section>

    <div class="button-container">
      <button class="save-recipe" v-on:click.prevent="saveRecipe" v-if="!item.saved"> Save Recipe</button>
      <button
        class="remove-recipe" v-on:click.prevent="removeRecipe" v-if="item.saved">Remove Recipe</button>
    </div>
  </section>
</template>

<script>
import Ingredient from "../components/Ingredient.vue";
import recipeService from "../services/RecipeService";

export default {
  name: "recipe",
  props: {
    item: Object,
    enableAdd: {
      type: Boolean,
      default: false,
    },
  },
  components: {
    Ingredient,
  },
  data() {
    return {
      recipe: {}
    }
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

    saveRecipe(item){
      let savedRecipe = Object.assign({saved:true}, item);
      this.$store.commit('SAVE_RECIPE', savedRecipe);
      
    },

    removeRecipe(item){
      let savedRecipe = Object.assign({saved:false}, item);
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
