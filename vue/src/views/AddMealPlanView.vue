<template>
    <h1>Add New Meal Plan</h1>
    <meal-plan-form v-bind:mealplan="mealplan" />
  </template>
  
  <script>
  import MealPlanForm from "../components/MealPlanForm.vue";
  
  export default {
    components: {
      MealPlanForm,
    },
    data() {
      return {
        mealplan: {
          id: 0,
          name: "",
          description: ""
        },
      };
    },
  };
  </script>
  