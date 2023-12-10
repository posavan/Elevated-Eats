<template>
  <h1>User Recipe Details</h1>
  <div class="Recipe Details">
    <recipe :key="recipe.recipeId" :item="recipe" />

  
  </div>
<<<<<<< HEAD
  <a href="/recipe/1/1/edit">Edit Recipe</a>
</template>

=======
</template>
  
>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
<script>
import recipe from "../components/Recipe.vue";
import recipeService from "../services/RecipeService.js";

export default {
  components: {
    recipe,
  },
<<<<<<< HEAD
  name: "RecipeDetailsView",
=======
  name: "UserRecipeDetailsView",
>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
  data() {
    return {
      recipe: [],
      recipeName: "",
      userId: 0,
<<<<<<< HEAD
      recipeId: 0,
=======
      recipeId: 0

>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
    };
  },
  methods: {
    loadRecipe() {
<<<<<<< HEAD
      console.log(this.userId, this.recipeId);
      recipeService
        .GetUserRecipeByRecipeId(this.recipeId, this.userId)
        .then((response) => {
          console.log("Reached created in RecipeDetailsView.vue");
=======
      console.log("reached userRecipeDetailsView/loadRecipe()", this.recipeId);
      recipeService
        .GetUserRecipeByRecipeId(this.recipeId)
        .then((response) => {
>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
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
<<<<<<< HEAD
    console.log("about to call load recipe");
    //this.loadRecipe(this.recipeName);
    this.recipeId = this.$route.params.recipeId;
    // this.userId = this.$store.params.userId;
=======
    this.recipeId = this.$route.params.recipeId;
>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
    this.loadRecipe();
  },
};
</script>
<<<<<<< HEAD

=======
  
>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
<style scoped>
h1 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
<<<<<<< HEAD
  background-color: wheat;
  border-radius: 50px;
}
=======
  background-color: rgb(206, 182, 236);
  border-radius: 50px;
}

>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
div {
  background-color: rgb(118, 244, 175);
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
  font-size: large;
  text-align: center;
  border-radius: 50px;
<<<<<<< HEAD

}
</style>
=======
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
  
>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
