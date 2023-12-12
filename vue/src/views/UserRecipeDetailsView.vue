<template>
  <h1>User Recipe Details</h1>
  <div class="User-Recipe-Details">
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
      recipeId: 0

    };
  },
  methods: {
    loadRecipe() {
      console.log(this.recipeId)
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
            console.log("Error loading recipe: unable to communicate to server");
          } else {
            console.log("Error loading recipe: make request");
          }
        });
    },
  },
  created() {
    console.log("about to call load recipe");
    this.recipeId = this.$route.params.recipeId;
    console.log(this.recipeId);
    //this.userId = this.$store.params.userId;
    this.loadRecipe();
  },
};
</script>

  
<style scoped>

h1{
  text-align: center;
}
div.User-Recipe-Details {
  background-color:  rgb(249, 205, 123);
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  text-align: center;
  border-right: 20px;
  border-color: black;
  border-width: 10rem;
  margin-top: 2%;
}

button {
  display: block;
    color: white;
    text-align: center;
    padding: 14px 16px;
    border-radius: 50px;
    margin-left: 40%;
    margin-right: 40%;
    border-top: none;
    border-left: none;
    border-right: none;
    margin-bottom: 0.9%;
    text-decoration: none;
    font-family: sans-serif;
    font-size: 16px;
}

button:hover {
  opacity: 0.8;
}
</style>
  

