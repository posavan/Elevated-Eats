<template>
  <div class="list-meals">
    <h1>Meals</h1>
    <section class="container">
      <meal v-for="meal in meals" v-bind:key="meal.id" v-bind:item="meal" />
    </section>
  </div>

</template>

<script>
import Meal from "../components/Meal.vue";
import MealService from "../services/MealService";

export default {
  components: {
    Meal,
  },
  name: "MealsView",
  props: ["item"],
  data() {
    return {
      meals: [],

    };
  },
  methods: {
    loadMeals() {
      MealService.list()
        .then((response) => {
          this.meals = response.data;
          this.$router.push("meal");
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading meals: ", error.response.status);
          } else if (error.request) {
            console.log("Error loading meals: unable to communicate to server");
          } else {
            console.log("Error loading meals: make request");
          }
        });
    },
  },
  created() {
    this.loadMeals();
  },
};
</script>
