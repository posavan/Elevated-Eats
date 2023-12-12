<template>
  <h1>Edit Meal: {{ editMeal.mealId }}</h1>
  <edit-meal-form v-bind:meal="editMeal.mealId" />
</template>

<script>
import MealService from "../services/MealService";
import EditMealForm from "../components/EditMealForm.vue";

export default {
  components: {
    EditMealForm,
  },
  data() {
    return {
      editMeal: {
        mealId: 0,
        mealName: '',
        mealDescription: '',
      },
    };
  },
  methods: {
    updateMeal() {
      MealService
        .updateMeal(this.editMeal)
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
    created() {
      this.editMeal.mealId = this.$route.params.mealId;
      console.log('logging editMeal', this.editMeal);
      this.editMeal.mealId = this.mealId;
      this.editMeal.mealDescription = this.mealDescription;
      MealService.getMeal(this.editMeal.mealId)
      .then((response) => {
          console.log(response.data);
          this.editMeal = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading meal: ", error.response.status);
          } else if (error.request) {
            console.log("Error loading meal: unable to communicate to server");
          } else {
            console.log("Error loading meal: make request");
          }
        });
    },
  },
};
</script>
