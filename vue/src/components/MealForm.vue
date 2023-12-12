<template>
    <form v-on:submit.prevent="createNewMeal">
      <div>
        <label for="name">Name: </label>
        <input type="text" name="name" id="name" v-model="editMeal.mealName" />
      </div>
      <div>
        <label for="description">Description: </label>
        <input type="text" name="description" id="description" v-model="editMeal.mealDescription" />
      </div>
      <!-- display recipes to be added -->
      <recipe v-for="recipe in addedRecipes" v-bind:key="recipe.recipeId" v-bind:item="recipe" />
      <div>
        <!-- add each recipe -->
        <label for="recipes">Recipes:</label>
        <select v-model="selected">
          <option v-for="recipe in recipes" v-bind:key="recipe.recipeId" :value="recipe">
            {{ recipe.recipeName }}
          </option>
        </select>
        <button class="btn-add-recipe" type="button" @click="addRecipe">Add Recipe</button>
      </div>
      <button class="btn-create-recipe" type="button" @click="$router.push({ name: 'createRecipe' })">Create New Recipe</button>
      <div class="actions">
        <button class="btn-submit" type="submit">Create Meal</button>
        <button class="btn-cancel" type="button" @click="cancelForm">Cancel</button>
      </div>
    </form>
  </template>
    
  <script>
  import mealService from "../services/MealService.js";
  import recipeService from "../services/RecipeService.js";
  import recipe from "../components/Recipe.vue";
  
  export default {
    components: {
      recipe
    },
    props: {
      meal: {
        type: Object,
        required: true,
      },
    },
    data() {
      return {
        selected: {},
        recipes: [],
        addedRecipes: [],
        editMeal: {
          mealId: this.meal.mealId,
          mealName: this.meal.mealName,
          mealDescription: this.meal.mealDescription,
          recipeList: this.meal.recipeList
        }
      };
    },
  
    methods: {
      loadRecipes() {
        recipeService
          .list()
          .then((response) => {
            this.recipes = response.data;
          })
          .catch((error) => {
            if (error.response) {
              console.log(
                "Error loading recipes: ",
                error.response.status
              );
            } else if (error.request) {
              console.log(
                "Error loading recipes: unable to communicate to server"
              );
            } else {
              console.log("Error loading recipes: make request");
            }
          });
      },
  
      addRecipe(){
        this.addedRecipes.push(this.selected);
      },
  
      createNewMeal() {
        this.editMeal.recipeList = this.addedRecipes;
        if (this.editMeal.mealName) {
          mealService
            .createMeal(this.editMeal)
            .then(() => {
              this.editMeal = {};
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
        }
      },
      cancelForm() {
        this.$router.back();
      },
    },
    created() {
      this.loadRecipes();
    },
  };
  </script>
    