<template>
    <form v-on:submit.prevent="createNewMealPlan">
      <div>
        <label for="name">Name: </label>
        <input type="text" name="name" id="name" v-model="editMealPlan.mealPlanName" />
      </div>
      <div>
        <label for="description">Description: </label>
        <input type="text" name="description" id="description" v-model="editMealPlan.mealPlanDescription" />
      </div>
      <div class="actions">
        <button class="btn-submit" type="submit">Add and Continue</button>
        <button class="btn-cancel" type="button" v-on:click="cancelForm">Return</button>
      </div>
    </form>
  </template>
  
  <script>
  import mealPlanService from "../services/MealPlanService.js";
  
  export default {
    props: {
      mealplan: {
        type: Object,
        required: true,
      },
    },
    data() {
      return {
        editMealPlan: {
          mealPlanId: this.mealplan.mealPlanId,
          mealPlanName: this.mealplan.mealPlanName,
          mealPlanDescription: this.mealplan.mealPlanDescription,
        }
      };
    },
  
    methods: {
      createNewMealPlan() {
        if (this.editMealPlan.mealPlanName) {
          mealPlanService
            .createMealPlan(this.editMealPlan)
            .then(() => {
              this.editMealPlan = {};
              this.$router.push({ name: "mealplan" });
            })
            .catch((error) => {
              if (error.response) {
                console.log("Error adding mealplan: ", error.response.status);
              } else if (error.request) {
                console.log(
                  "Error adding mealplan: unable to communicate to server"
                );
              } else {
                console.log("Error adding mealplan: make request");
              }
            });
        }
      },
      cancelForm() {
        this.$router.back();
      },
    },
  };
  </script>
  