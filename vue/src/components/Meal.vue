<template>
  <section class="meal">
    <h2>Meal: {{ item.mealName }}</h2>
    <p>Description: {{ item.mealDescription }}</p>
    <section v-if="inDetails" class="container">
      <p>Recipes:</p>
      <recipe v-for="recipe in item.recipeList" v-bind:key="recipe.recipeId" v-bind:item="recipe" />
    </section>
    <div class="button-container">
      <button class="view-meal-details" v-on:click="this.$router.push(`/meal/${this.mealId}`)"
        v-if="!inDetails && !inAdd">
        View Meal Details
      </button>
      <button class="edit-meal" v-on:click="this.$router.push({ name: 'EditMealView', params: { mealId: this.mealId } })"
        v-if="inEdit || inDetails">Edit Meal
      </button>
      <button class="delete-meal" v-on:click.prevent="deleteMeal" v-if="!show && !inDetails">
        Delete Meal
      </button>
      <button class="btn-cancel" type="button" @click="cancel" v-if="inDetails || inCreate">
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
      inDetails: this.$route.name == "mealDetailsView",
      inEdit: this.$route.name == "editMealPlan",
      inAdd: this.$route.name == "addMealPlan",
      inCreate: this.$route.name == "createMeal",
      show: this.$route.name == "meal",
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
          location.reload();
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
    console.log("meal item", this.item);
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
  text-align: center;
}

.meal {
  border: solid black;
  /* background-color: rgb(123, 249, 188); */
  border-radius: 0.5rem;
  margin-bottom: 1.25rem;
  width: 97%;
  padding: 1%;
  border-left: 10%;
  border-right: 10%;

}

.container {
  padding: 10%;
  padding-top: .05%;
}

.meal:hover {
  color: black;
}

button {
  font-size: 16px;
  font-weight: 200;
  letter-spacing: 1px;
  padding: 13px 20px 13px;
  outline: 0;
  border: 1px solid black;
  cursor: pointer;
  position: relative;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
}

button:after {
  content: "";
  background-color:rgb(254, 178, 108);
  width: 100%;
  z-index: -1;
  position: absolute;
  height: 100%;
  top: 7px;
  left: 7px;
  transition: 0.2s;
}

button:hover:after {
  top: 0px;
  left: 0px;
}

@media (min-width: 768px) {
  button{
    padding: 13px 50px 13px;
  }
}
</style>
