<template>
  <section class="meal">
    <h2>Meal Name: {{ item.mealName }}</h2>
    <p>Description: {{ item.mealDescription }}</p>
    <section v-if="showRecipes" class="container">
      <p>Recipes:</p>
      <recipe v-for="recipe in item.recipeList" v-bind:key="recipe.recipeId" v-bind:item="recipe" />
    </section>
    <div class="button-container">
      <button class="edit-meal" v-on:click="this.$router.push('/meal/' + this.mealId + '/edit')" v-if="showEdit">
        Edit Meal
      </button>
      <button class="view-meal-details" v-on:click="this.$router.push('/meal/' + this.mealId)" v-if="showDetails || show">
        View Meal Details
      </button>
    </div>
  </section>
</template>

<script>
import recipe from "../components/Recipe.vue";
import RecipeService from "../services/RecipeService";
import MealService from "../services/MealService.js";
import MealPlanService from "../services/MealPlanService";

export default {
  components: {
    recipe,
  },
  name: "meal",
  props: ["item"],
  data() {
    return {
      meal: {},
      recipes: [],
      mealId: 0,
      mealName: "",
      mealDescription: "",
      showDetails: this.$route.name == "meal",
      showRecipes: this.$route.name == "mealDetailsView",
      showEdit: this.$route.name == "EditMealView",
      show: this.$route.name == "editMealPlan"
    };
  },
  methods: {
    loadMeals() {
      console.log(this.meal, "this meal");
      MealService.list(this.meal.mealId)
        .then((response) => {
          console.log("response", response);
          this.meal = response.data;
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
    loadRecipes() {
      console.log(this.meal, "this meal");
      MealService.listRecipesFromMeal(this.meal.mealId)
        .then((response) => {
          console.log("response", response);
          this.recipes = response.data;
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
    // loadMealRecipes() {
    //   MealService.listRecipesFromMeal(this.item.mealId)
    //     .then((response) => {
    //       console.log("successful recipe/loadRecipeIngredients");
    //       this.ingredients = response.data;
    //     })
    //     .catch((error) => {
    //       if (error.response) {
    //         console.log(
    //           "Error loading recipe ingredients: ",
    //           error.response.status
    //         );
    //       } else if (error.request) {
    //         console.log(
    //           "Error loading ingredients: unable to communicate to server"
    //         );
    //       } else {
    //         console.log("Error loading ingredients: make request");
    //       }
    //     });
    // },
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
  },
  created() {
    console.log(" step 4 reached created load recipes");
    this.meal = this.item;

    console.log("logging this meal", this.meal)
    this.mealId = this.meal.mealId;
    if (this.$route.name == "meal") {
      this.loadMeals();
    } else if (this.$route.name == "mealDetailsView") {
      if (this.meal.recipeList) {
        this.loadRecipes();
      }
    }
  },
};
</script>

<style>
h1 {
  text-align: right;
}

.meal {
  border: solid black;
  background-color: rgb(249, 205, 123);
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
