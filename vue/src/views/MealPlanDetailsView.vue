<template>
    <h1>Meal Plan Details</h1>
    <div class="Meal Plan Details">
      <mealplan :key="mealplan.mealPlanId" :item="mealplan" />
    </div>
  </template>
    
  <script>
  import mealplan from "../components/MealPlan.vue";
  import mealPlanService from "../services/MealPlanService.js";
  
  export default {
    components: {
      mealplan,
    },
    name: "MealPlanDetailsView",
    data() {
      return {
        mealplan: {},
        mealPlanId: 0
      };
    },
    methods: {
      loadMealPlan() {
        mealPlanService
          .getMealPlan(this.mealPlanId)
          .then((response) => {
            console.log(response.data);
            this.mealplan = response.data;
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error loading mealplan: ", error.response.status);
            } else if (error.request) {
              console.log("Error loading mealplan: unable to communicate to server");
            } else {
              console.log("Error loading mealplan: make request");
            }
          });
      },
    },
    created() {
      this.mealPlanId = this.$route.params.mealPlanId;
      this.loadMealPlan();
    },
  };
  </script>
  
    
  <style scoped>
  div {
    background-color: rgb(238, 221, 127);
    font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
    font-size: large;
    border: solid black;
    border-radius: 50px;
  }
  h1{
    text-align: center;
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
    
  
  