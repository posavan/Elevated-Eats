<template>
    <h1>Create New Meal</h1>
    <meal-form v-bind:meal="meal" />
  </template>
  
  <script>
  import mealForm from "../components/MealForm.vue";
  
  export default {
    components: {
      mealForm,
    },
    data() {
      return {
        meal: {
          id: 0,
          name: "",
          description: "",
          recipeList:[]
        },
      };
    },
  };
  </script>