<template>
  <div class="list-meals">
    <h1>Meals</h1>
    <section class="container">
      <Meal v-for="meal in meals" v-bind:key="meal.mealId" v-bind:item="meal" />
    </section>
    <div class="create-meal">
            <button v-on:click="$router.push({ name: 'createMeal' })">Create Meal</button>
        </div>
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
      MealService
        .list()
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
<style>
button {
  display: inline-block;
  border-radius: 1.5rem;
  cursor: pointer;
  padding: 0.5rem 1.5rem;
  text-decoration: none;
  white-space: wrap;
  text-transform: none;
  font-family: FuturaPT, helvetica, sans-serif;
  font-feature-settings: normal;
  font-style: normal;
  letter-spacing: normal;
  line-break: auto;
  line-height: 1.25em;
  font-size: 16px;
  font-weight: 500;
  overflow-wrap: normal;
  border-width: .2rem;
  margin-bottom: 1.9rem;
  text-align: center;
}
button:hover {
  border-style: dotted;
}
section.meal{
  background-color: rgb(255, 207, 111);
  
}
</style>
