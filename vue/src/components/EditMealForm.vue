<template>
  <form v-on:submit.prevent="updateMeal">
    <div>
      <label for="name">Name: </label>
      <input type="text" name="name" id="name" v-model="editMeal.mealName" />
    </div>
    <div>
      <label for="description">Description: </label>
      <input type="text" name="description" id="description" v-model="editMeal.mealDescription" />
    </div>
    <div v-for="recipe in Array.from(editMeal.recipeList)" :key="recipe.recipeId">
      <label for="type">Edit Recipe Name: </label>
      <input type="text" name="edit-recipe-name" id="edit-recipe-name" v-model="recipe.recipeName" />

      <label for="type">Edit Recipe Description: </label>
      <input type="text" name="edit-recipe-description" id="edit-recipe-description" v-model="recipe.recipeDescription" />
    </div>

    <recipe v-for="recipe in addedRecipes" v-bind:key="recipe.recipeId" v-bind:item="recipe">
    </recipe>
    <div>
      <label for="recipe">Recipe:</label>
      <select v-model="newRecipe">
        <option v-for="recipe in recipes" :key="recipe.recipeId" :value="recipe">
          {{ recipe.recipeName }}
        </option>
      </select>
      <button type="button" v-on:click="addRecipe">
        Add Recipe
      </button>
    </div>
    <div v-if="showRecipeForm">
      <label for="type">New Recipe Name: </label>
      <input type="text" name="new-recipe-name" id="new-recipe-name" v-model="newRecipe.recipeName" />
      <label for="type">New Recipe Description: </label>
      <input type="text" name="new-recipe-description" id="new-recipe-description"
        v-model="newRecipe.recipeDescription" />
      <button v-on:click.prevent="addNewRecipe">Add +</button>
    </div>
    <button type="button" v-on:click="showRecipeForm = !showRecipeForm">
      Create New Recipe
    </button>

    <div class="actions">
      <button class="btn-submit" type="submit">Add and Continue</button>
      <button class="btn-cancel" type="button" v-on:click="cancelForm">
        Return
      </button>
    </div>
  </form>
</template>

<script>
import mealService from "../services/MealService.js";
import recipeService from "../services/RecipeService.js";
import recipe from "../components/Recipe.vue";

export default {
  components: { recipe },
  props: {
    meal: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      recipes: [],
      addedRecipes: [],
      editMeal: {
        mealId: this.meal.mealId,
        mealName: this.meal.mealName,
        mealDescription: this.meal.mealDescription,
        recipeList: this.meal.recipeList
      },
      showRecipeForm: false,
      newRecipe: {
        recipeId: 0,
        recipeName: "",
        recipeDescription: "",
        ingredientList: []
      }
    };
  },

  methods: {
    loadRecipes() {
      recipeService
        .listUserRecipes()
        .then((response) => {
          this.recipes = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading recipes: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error loading recipes: unable to communicate to server"
            );
          } else {
            console.log("Error loading recipes: make request");
          }
        });
    },
    addRecipe() {
      this.addedRecipes.push(
        this.newRecipe
      );
      this.newRecipe = {};
    },
    addRecipesToMeal() {
      this.editMeal.recipeList = this.addedRecipes;
      this.addedRecipes.forEach(addedRecipe => {
        console.log(addedRecipe, this.editMeal);
        mealService
          .addRecipeToMeal(this.editMeal.mealId, addedRecipe)
          .then((response) => {
            console.log(response);
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error adding meal: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error adding meal: unable to communicate to server"
              );
            } else {
              console.log("Error adding meal: make request");
            }
          });
      });
    },

    updateMeal() {
      this.addRecipesToMeal();
      mealService
        .updateMeal(this.editMeal.mealId, this.editMeal)
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
    addNewRecipe() {
      recipeService
        .createRecipe(this.newRecipe)
        .then((response) => {
          this.newRecipe = response.data;
          this.addedRecipes.push(
            this.addRecipe
          );
          this.newRecipe = {};
          console.log("Recipe added successfully", response.data);
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error adding recipe: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error adding recipe: unable to communicate to server"
            );
          } else {
            console.log("Error adding recipe: make request");
          }
        });
      },

    cancelForm() {
      this.$router.back();
    },
  },

  created(){
    console.log("reached edit meal form");
    this.editMeal = this.meal;
    this.loadRecipes();
  }
};
</script>
