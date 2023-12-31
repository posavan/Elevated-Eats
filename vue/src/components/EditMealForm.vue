<template>
  <form v-on:submit.prevent="updateMeal">
    <div>
      <label for="name">Name: </label>
      <input type="text" name="name" id="name" v-model="editMeal.mealName" />
    </div>
    <div>
      <label for="description">Description: </label>
      <textarea name="description" id="description" v-model="editMeal.mealDescription" />
    </div>
    <div>
      <label for="image">Edit Image URL: </label>
      <input type="text" name="edit-image" id="edit-image" v-model="editMeal.mealImage" />
    </div>
    <h3>Recipes:</h3>
    <div v-for="recipe in Array.from(editMeal.recipeList)" :key="recipe.recipeId">
      <p>{{ recipe.recipeName }}</p>
      <button @click.prevent="removeRecipe(recipe)">Remove</button>
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
        Add Recipe +
      </button>
    </div>
    <button type="button" v-on:click="buttonClick">
      {{ feedback }}
    </button>
    <div v-if="showRecipeForm">
      <label for="type">New Recipe: </label>
      <input type="text" name="new-recipe-name" id="new-recipe-name" v-model="newRecipe.recipeName" />
      <label for="type">New Recipe Instructions: </label>
      <textarea placeholder="instructions" name="new-recipe-instructions" id="new-recipe-instructions"
        v-model="newRecipe.recipeInstructions" />
      <label for="type">New Recipe Image URL: </label>
      <input type="text" name="new-recipe-image" id="new-recipe-image" v-model="newRecipe.recipeImage" />
      <button v-on:click.prevent="addNewRecipe">Create</button>
    </div>

    <div class="actions">
      <button class="btn-submit" type="submit">Save Meal</button>
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
        recipeInstructions: "",
        ingredientList: [],
        recipeImage: ""
      },
      feedback: "Create New Recipe"
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

    addNewRecipe() {
      recipeService
        .createRecipe(this.newRecipe)
        .then((response) => {
          console.log("created", response)
          this.newRecipe = response.data;
          this.addedRecipes.push(
            this.newRecipe
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
    addRecipe() {
      this.addedRecipes.push(
        this.newRecipe
      );
      this.newRecipe = {};
    },
    removeRecipe(targetRecipe) {
      console.log(targetRecipe);
      mealService
        .removeRecipeFromMeal(this.editMeal.mealId, targetRecipe.recipeId)
        .then((response) => {
          console.log("Recipe removed successfully", response.data);
          location.reload();
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
        .updateMeal(this.editMeal)
        .then((response) => {
          this.$router.push({
            name: "mealDetailsView",
            params: { mealId: this.editMeal.mealId },
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

    buttonClick() {
      this.showRecipeForm = !this.showRecipeForm;
      if (this.showRecipeForm) {
        this.feedback = "Collapse";
      } else {
        this.feedback = "Create New Recipe";
      }
    },

    cancelForm() {
      this.$router.back();
    },
  },

  created() {
    console.log("reached edit meal form");
    this.editMeal = this.meal;
    this.loadRecipes();
  }
};
</script>
