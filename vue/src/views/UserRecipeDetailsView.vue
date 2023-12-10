<template>
  <h1>User Recipe Details</h1>
  <div class="Recipe Details">
    <recipe :key="recipe.recipeId" :item="recipe" />
  </div>
  <a href="/recipe/1/1/edit">Edit Recipe</a>
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
      userId: 0,
      recipeId: 0,
    };
  },
  methods: {
    loadRecipe() {
      console.log(this.userId, this.recipeId);
      recipeService
        .GetUserRecipeByRecipeId(this.recipeId, this.userId)
        .then((response) => {
          console.log("Reached created in RecipeDetailsView.vue");
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
    console.log("about to call load recipe");
    //this.loadRecipe(this.recipeName);
    this.recipeId = this.$route.params.recipeId;
    // this.userId = this.$store.params.userId;
    this.loadRecipe();
  },
};
</script>

<style scoped>
h1 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
  background-color: wheat;
  border-radius: 50px;
}
div {
  background-color: rgb(118, 244, 175);
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
  font-size: large;
  text-align: center;
  border-radius: 50px;

}
</style>
