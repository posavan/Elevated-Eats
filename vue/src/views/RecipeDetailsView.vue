<template>
  <h1>Recipe Details</h1>
  <div class="Recipe Details">
    <recipe :key="recipe.recipeName" :item="recipe" />
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
      recipe: [],
      recipeName: "",
    };
  },
  methods: {
    loadRecipe() {
      console.log(this.recipeName)
      recipeService
        .listRecipeByName(this.recipeName)
        .then((response) => {
          console.log(response.data);
          this.recipe = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading recipe: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error loading recipe: unable to communicate to server"
            );
          } else {
            console.log("Error loading recipe: make request");
          }
        });
    },
  },
  created() {
    this.recipeName = this.$route.params.recipeName;
    this.loadRecipe();
  },
};
</script>

<style scoped>
/* h1 {
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
} */
</style>
