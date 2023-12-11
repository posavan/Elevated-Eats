<template>
  <form v-on:submit.prevent="createNewMeal">
    <div>
      <label for="name">Name: </label>
      <input type="text" name="name" id="name" v-model="editMeal.mealName" />
    </div>
    <div>
      <label for="description">Description: </label>
      <input
        type="text"
        name="description"
        id="description"
        v-model="editMeal.mealDescription"
      />
    </div>
    <div class="actions">
      <button class="btn-submit" type="submit">Add and Continue</button>
      <button class="btn-cancel" type="button" v-on:click="cancelForm">
        Return
      </button>
    </div>
  </form>
</template>

<script>
import MealService from "../services/MealService.js";

export default {
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
  },
};
</script>
