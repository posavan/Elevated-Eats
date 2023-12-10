<template>
  <h1>User Recipe Details</h1>
  <div class="Recipe Details">
    <recipe :key="recipe.recipeId" :item="recipe" />

  </div>
<<<<<<< HEAD
</template>
  
=======

</template>

>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
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

>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
  data() {
    return {
      recipe: [],
      recipeName: "",
      userId: 0,
<<<<<<< HEAD
      recipeId: 0,
=======
      recipeId: 0
>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
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
>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
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
>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
    this.loadRecipe();
  },
};
</script>

<<<<<<< HEAD
  
=======
>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
<style scoped>
h1 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
<<<<<<< HEAD
  background-color: wheat;
=======
  background-color: rgb(206, 182, 236);
>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
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
<<<<<<< HEAD
  
=======

>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
