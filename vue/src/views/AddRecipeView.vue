<template>
    <h1>Add New Recipe</h1>
    <recipe-form v-bind:recipe="recipe" />
  </template>
  
  <script>
  import RecipeForm from "../components/RecipeForm.vue";
  
  export default {
    components: {
      RecipeForm,
    },
    data() {
      return {
        recipe: {
          id: 0,
          name: "",
          instructions: "",
          mealId: Number(this.$route.params.mealId)
        },
      };
    },
  };
  </script>
  