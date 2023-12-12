<template>
  <section class="meal">
    <h2>Meal Name: {{ item.mealName }}</h2>
    <p>Description: {{ item.mealDescription }}</p>
    <section v-if="!showDetails" class="container">
      <p>Recipes: {{ item.recipeList }}</p>
      <!-- <recipe
        v-for="recipe in recipes"
        v-bind:key="recipe.recipeId"
        v-bind:item="recipe"
      /> -->
    </section>
    <div class="button-container">
      <button
        class="edit-meal"
        v-on:click="this.$router.push('/meal/' + this.mealId + '/edit')"
        v-if="showEdit"
      >
        Edit Meal
      </button>
      <button
        class="view-meal-details"
        v-on:click="this.$router.push('/meal/' + this.mealId)"
        v-if="showDetails"
      >
        View Meal Details
      </button>
    </div>
  </section>
</template>

<script>
import Recipe from "../components/Recipe.vue";
import RecipeService from "../services/RecipeService";
import MealService from "../services/MealService.js";
import MealPlanService from "../services/MealPlanService";

export default {
  // components: {
  //   Recipe,
  // },
  name: "meal",
  props: ["item"],
  data() {
    return {
      meals: {},
      mealId: 0,
      mealName: "",
      mealDescription: "",
      showDetails: this.$route.name == "meal",
      showEdit: this.$route.name == "mealEdit",
    };
  },
  loadMeals() {
    MealService.list()
      .then((response) => {
        this.meals = response.data;
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
  //   loadMealRecipes() {
  //       MealService
  //         .listRecipesByMeal(this.recipe.recipeId)
  //         .then((response) => {
  //           console.log("successful recipe/loadRecipeIngredients");
  //           this.ingredients = response.data;
  //         })
  //         .catch((error) => {
  //           if (error.response) {
  //             console.log(
  //               "Error loading recipe ingredients: ",
  //               error.response.status
  //             );
  //           } else if (error.request) {
  //             console.log(
  //               "Error loading ingredients: unable to communicate to server"
  //             );
  //           } else {
  //             console.log("Error loading ingredients: make request");
  //           }
  //         });
  //     },
  saveMeal() {
    MealPlanService.addMealToMealPlan(this.meal)
      .then((response) => {
        console.log(response);
        this.$router.push({ name: "meal" });
        this.buttonClick();
        //location.reload();
      })
      .catch((error) => {
        if (error.response) {
          console.log("Error saving meal: ", error.response.status);
        } else if (error.request) {
          console.log("Error saving meal: unable to communicate to server");
        } else {
          console.log("Error saving meal: make request");
        }
      });
  },
  buttonClick() {
    this.feedback = "Added";
  },
  created() {
    this.meal = this.item;
    this.mealId = this.item.mealId;
  },
};
</script>

<style>
h1 {
  text-align: right;
}

.meal {
  background-color: rgb(132, 226, 170);
  border-radius: 0.5rem;
  margin-bottom: 1.25rem;
  width: 97%;
  padding: 1%;
  border-left: 10%;
  border-right: 10%;
}
.meal:hover {
 
  color: black;
}
</style>
