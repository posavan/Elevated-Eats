<!-- IN PROGRESS -->


<template>
  <div class="list-recipe-ingredients">
    <h1>Recipe Ingredients</h1>
    <section class="container">
      <ingredient
        v-for="ingredient in ingredients"
        v-bind:key="ingredient.id"
        v-bind:item="ingredient"
      />
    </section>
  </div>
</template>

<script>
import ingredient from "../components/Ingredient.vue";
import RecipeService from "../services/RecipeService";

export default {
  name: "listRecipeIngredients",
  components: {
    ingredient
  },
  data() {
    return {
      ingredients: [],
      recipeId: 0
    };
  },

  methods: {
    loadIngredients() {
        RecipeService
        .listIngredients(this.recipeId)
        .then((response) => {
            console.log(response);
            this.ingredients = response.data;
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
    this.recipeId = this.$route.params.recipeId;
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
