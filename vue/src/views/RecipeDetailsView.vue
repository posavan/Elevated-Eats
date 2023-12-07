<template>
  <h1>Recipe Details</h1>
  <div class="Recipe Details">
    <recipe v-if="recipe" :key="recipe.id" :item="recipe" />
  </div>
</template>

<script>
import recipe from "../components/Recipe.vue";
import recipeService from "../services/RecipeService.js";

export default {
  components: {
    recipe,
  },
  name: "RecipeDetailsView",
  data() {
    return {
      recipe: null,
    };
  },
  methods: {
    loadRecipe(recipeId) {
      recipeService
        .list() /// get recipe by id
        .then((response) => {
          console.log("Reached created in RecipeDetailsView.vue");
          console.log(response);
          if (response.data && response.data.length > 0) {
            this.recipe = response.data[0];
          }
          //this.recipe = response.data;
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error loading recipe: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log(
              "Error loading recipe: unable to communicate to server"
            );
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error loading recipe: make request");
          }
        });
    },
  },
  created() {
    this.loadRecipe(this.recipeId);
    //const recipeId = this.$route.params.recipeId;
    //this.loadRecipe(recipeId);
  },
};
</script>

<style scoped>
h1 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
  background-color: rgb(210, 184, 244);
  border-radius: 50px;
}
div {
  background-color: rgb(167, 116, 244);
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
  font-size: large;
  text-align: center;
  border-radius: 50px;
  padding: auto;
}
</style>
