<template>
  <section class="meal">
    <h2>Meal Name: {{ item.mealName }}</h2>
    <p>Description: {{ item.mealDescription }}</p>
    <section v-if="showRecipes" class="container">
      <p>Recipes:</p>
      <recipe v-for="recipe in item.recipeList" v-bind:key="recipe.recipeId" v-bind:item="recipe" />
    </section>
    <div class="button-container">
      <button class="view-meal-details" v-on:click="this.$router.push(`/meal/${this.mealId}`)" v-if="showDetails || show">
        View Meal Details
      </button>
      <button class="edit-meal" v-on:click="this.$router.push(`/meal/${this.mealId}/edit`)" v-if="showRecipes">
        Edit Meal
      </button>
      <button class="delete-meal" v-on:click.prevent="deleteMeal" v-if="showRecipes">
        Delete Meal Plan
      </button>
      <button class="btn-cancel" type="button" @click="cancel" v-if="!showDetails">
        Return
      </button>
    </div>
  </section>
</template>

<script>
import recipe from "../components/Recipe.vue";
import RecipeService from "../services/RecipeService";
import mealService from "../services/MealService.js";
import mealPlanService from "../services/MealPlanService";

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
      show: this.$route.name == "editMealPlan",
    };
  },
  methods: {
    // loadMeals() {
    //   mealService
    //     .list(this.meal.mealId)
    //     .then((response) => {
    //       console.log("response", response);
    //       this.meal = response.data;
    //     })
    //     .catch((error) => {
    //       if (error.response) {
    //         console.log("Error loading meals: ", error.response.status);
    //       } else if (error.request) {
    //         console.log("Error loading meals: unable to communicate to server");
    //       } else {
    //         console.log("Error loading meals: make request");
    //       }
    //     });
    // },
    loadRecipes() {
      mealService
        .listRecipesFromMeal(this.meal.mealId)
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

    // saveMeal() {
    //   mealService
    //     .createMeal(this.meal)
    //     .then((response) => {
    //       console.log(response);
    //       this.$router.push({ name: "meal" });
    //       this.buttonClick();
    //       //location.reload();
    //     })
    //     .catch((error) => {
    //       if (error.response) {
    //         console.log("Error saving meal: ", error.response.status);
    //       } else if (error.request) {
    //         console.log("Error saving meal: unable to communicate to server");
    //       } else {
    //         console.log("Error saving meal: make request");
    //       }
    //     });
    // },
    deleteMeal() {
      mealService
        .deleteMeal(this.meal.mealId)
        .then((response) => {
          console.log(response);
          // if (!this.show) {
          //     this.cancel();
          // }
          this.cancel();
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error removing mealplan: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error removing mealplan: unable to communicate to server"
            );
          } else {
            console.log("Error removing mealplan: make request");
          }
        });
    },
    cancel() {
      this.$router.back();
    },
    buttonClick() {
      this.feedback = "Added";
    },
  },
  created() {
    this.meal = this.item;
    this.mealId = this.meal.mealId;
    // if (this.$route.name == "meal") {
    //   this.loadMeals();
    // } else 
    // if (this.$route.name == "mealDetailsView") {
    if (this.meal.recipeList) {
      this.loadRecipes();
    }

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
