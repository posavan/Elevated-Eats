<template>
  <h1>Edit Meal</h1>
  <edit-meal-form v-bind:meal="meal" />
</template>

<script>
import MealService from "../services/MealService";
import EditMealForm from "../components/EditMealForm.vue";

export default {
  components: {
    EditMealForm,
  },

  props: {
    meal: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      editMeal: {
        mealId: this.meal.mealId,
        mealName: this.meal.mealName,
        mealDescription: this.meal.mealDescription,
      },
    };
  },
  methods: {
    updateMeal() {
      MealService.updateMeal(this.editMeal.mealId, this.editMeal)
        .then(() => {
          this.$router.push({
            name: "mealDetailsView",
            params: { id: this.editMeal.mealId },
          });
          console.log("Meal edited successfully");
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error updating Meal: ", error.response.status);
          } else if (error.request) {
            console.log("Error updating Meal: unable to communicate to server");
          } else {
            console.log("Error updating Meal: make request");
          }
        });
    },

    cancelForm() {
      this.$router.back();
    },
  }
};
</script>
