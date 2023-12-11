<template>
  <section class="meal">
    <h2>{{ item.mealName }}</h2>
    <p>Description: {{ item.mealDescription }}</p>
    <!-- <p>Recipes:</p>
    <recipe
      v-for="recipe in recipes"
      v-bind:key="recipe.recipeId"
      v-bind:item="recipe"
    /> -->
    <div class="button-container">
      <button class="save-meal" v-on:click.prevent="saveMeal" v-if="!hide">
       Save Meal {{ feedback }}
      </button>
      <button class="remove-meal" v-on:click.prevent="removeMeal" v-if="!hide">
        Delete Meal
      </button>
    </div>
  </section>
</template>

<script>
import RecipeService from "../services/RecipeService";
import MealService from "../services/MealService.js";

export default {
  name: "meal",
  props: ["item"],
  data() {
    return {
      meals: {},
      mealId: 0,
      mealName: "",
      mealDescription: "",
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
  saveMeal() {
      MealService
        .addMealToMealPlan(this.recipe)
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
    removeMeal() {
      MealService
        .removeMeal(this.mealId)
        .then((response) => {
          console.log(response);
          //this.$router.push({name: 'favorites' });
          location.reload();
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error removing meal: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error removing meal: unable to communicate to server"
            );
          } else {
            console.log("Error removing meal: make request");
          }
        });
    },
    buttonClick(){
        this.feedback = "Added";
    }
};
</script>

<style>
h1 {
  text-align: center;
}

.meal {
  background-color: rgb(171, 247, 201);
  border-radius: 0.5rem;
  margin-bottom: 1.25rem;
  width: 100%;
  padding: 2%;
}
.meal:hover {
  opacity: 50%;
}

</style>
