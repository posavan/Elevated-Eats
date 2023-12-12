<template>
  <h1>Meal Details</h1>
  <div class="Meal Details">
    <meal :key="meal.mealId" :item="meal" />
  </div>
  <div class="edit-meal"> 
  <button v-on:click="this.$router.push('/meal/' + this.mealId + '/edit')">Edit Meal Details</button>
  </div>
  <div class="remove-meal">
    <button v-on:click.prevent="removeMeal" v-if="!hide">Delete Meal</button>
  </div>
</template>

<script>
import Meal from "../components/Meal.vue";
import MealService from "../services/MealService";

export default {
  components: {
    Meal,
  },
  name: "MealDetailsView",
  data() {
    return {
      meal: [],
      mealName: "",
      mealId: 0,
    };
  },
  methods: {
    loadMeal() {
      console.log(this.mealId);
      MealService.getMeal(this.mealId)
        .then((response) => {
          console.log(response.data);
          this.meal = response.data;
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
    removeMeal() {
      MealService.deleteMeal(this.mealId)
        .then((response) => {
          console.log(response);
          //this.$router.push({name: 'favorites' });
          location.reload();
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error removing meal: ", error.response.status);
          } else if (error.request) {
            console.log("Error removing meal: unable to communicate to server");
          } else {
            console.log("Error removing meal: make request");
          }
        });
    },
  },
  created() {
    this.mealId = this.$route.params.mealId;
    this.loadMeal();
  },
};
</script>

<style scoped>
 h1{
  text-align: center;
 }
div {
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
  font-size: large;
  text-align: center;
  border-radius: 50px;
  padding: auto;
}

button.remove-meal {
  align-items: right;
}
.edit-meal-details{
    align-items: right;
}
</style>
