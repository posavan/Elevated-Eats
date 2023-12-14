<template>
  <h1 v-if="isLoading == false">Edit Meal: {{ editMeal.mealName }}</h1>
  <edit-meal-form v-bind:meal="editMeal" v-if="isLoading == false" />
</template>

<script>
import mealService from "../services/MealService";
import EditMealForm from "../components/EditMealForm.vue";

export default {
  components: {
    EditMealForm,
  },
  prop: {
    meal: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      isLoading: true,
      editMeal: {
        mealId: this.mealId,
        mealName: this.mealName,
        mealDescription: this.mealDescription,
      },
    };
  },
  methods: {
    loadMeal() {
      mealService
        .getMeal(this.editMeal.mealId)
        .then((response) => {
          console.log(response.data);
          this.editMeal = response.data;
          this.isLoading = false;
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
    updateMeal() {
      mealService
        .updateMeal(this.editMeal)
        .then((response) => {
          this.$router.push({
            name: "mealDetailsView",
            params: { id: this.editMeal.mealId },
          });
          console.log("Meal edited successfully", response);
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

  },
  created() {
      console.log("reached edit meal view");
      this.editMeal.mealId = this.$route.params.mealId;
      this.loadMeal();
      // this.editMeal.mealId = this.mealId;
      // this.editMeal.mealDescription = this.mealDescription;
      // MealService.getMeal(this.editMeal.mealId)
      //   .then((response) => {
      //     console.log(response.data);
      //     this.editMeal = response.data;
      //   })
      //   .catch((error) => {
      //     if (error.response) {
      //       console.log("Error loading meal: ", error.response.status);
      //     } else if (error.request) {
      //       console.log("Error loading meal: unable to communicate to server");
      //     } else {
      //       console.log("Error loading meal: make request");
      //     }
      //   });
    },
};
</script>
