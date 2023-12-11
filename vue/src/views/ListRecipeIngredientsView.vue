<!-- IN PROGRESS -->


<template>
  <div class="list-recipe-ingredients">
    <h1>Recipe Ingredients</h1>
    <section class="container">
      <ingredient
        v-for="recipe in recipes"
        v-bind:key="recipe.id"
        v-bind:item="recipe.ingredientName"
      />
    </section>
  </div>
</template>

<script>
import recipe from "../components/Recipe.vue";
import ingredient from "../components/Ingredient.vue";
import RecipeService from "../services/RecipeService";
import IngredientService from "../services/IngredientService";

export default {
  name: "listRecipeIngredients",
  components: {
    ingredient
  },
  data() {
    return {
      ingredients: [],
      showForm: false,
      newIngredient: {}
    };
  },

  methods: {
    loadIngredients() {
        RecipeService
        .listIngredients(this.recipeId)
        .then((response) => {
            console.log(response);
            this.recipes = response.data;
            this.$router.push('listRecipeIngredients');
        })
        .catch((error) => {
            if (error.response) {
                console.log("Error loading ingredients: ", error.response.status); 
            } else if (error.request) {
                console.log("Error loading ingredients: unable to communicate to server");
            } else {
                console.log("Error loading ingredients: make request")
            }
        })
    }
  },

  created() {
    this.loadIngredients();
  }
};
</script>

<style scoped>
h1 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
  background-color: rgb(206, 182, 236);
  border-radius: 50px;
}

div {
  background-color: rgb(118, 244, 175);
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
  font-size: large;
  text-align: center;
  border-radius: 50px;
  padding: auto;
}
</style>
