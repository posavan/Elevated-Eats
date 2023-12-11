<template>
  <h1>User Recipe Details</h1>
  <div class="Recipe Details">
    <recipe :key="recipe.recipeId" :item="recipe" />

  
  </div>

</template>

<script>
import recipe from "../components/Recipe.vue";
import recipeService from "../services/RecipeService.js";

export default {
  components: {
    recipe,
  },
  name: "UserRecipeDetailsView",
  data() {
    return {
      recipe: [],
      recipeName: "",
      userId: 0,
      recipeId: 0
    };
  },
  methods: {
    loadRecipe() {
      console.log("reached userRecipeDetailsView/loadRecipe()", this.recipeId);
      recipeService
        .GetUserRecipeByRecipeId(this.recipeId)
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

    this.recipeId = this.$route.params.recipeId;
    this.loadRecipe();
  },
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
  justify-content: space-between;
}

button {
  padding: 1.25rem;
  background-color: #4caf50;
  justify-content: space-between;
  color: #fff;
  border: none;
  border-radius: 0.25rem;
  cursor: pointer;
}

button:hover {
  opacity: 0.8;
}
</style>

