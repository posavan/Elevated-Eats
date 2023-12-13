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
      <select v-model="newRecipe">
        <option v-for="recipe in recipes" v-bind:key="recipe.recipeId" :value="recipe">
          {{ recipe.recipeName }}
        </option>
      </select>
      <button class="btn-add-recipe" type="button" @click="addRecipe">Add Recipe</button>
    </div>
    <div v-if="showRecipeForm">
      <label for="name">New Recipe Name:</label>
      <input type="text" name="new-recipe-name" id="new-recipe-name" v-model="newRecipe.recipeName" />
      <label for="instructions">New Recipe Instructions:</label>
      <input type="text" name="new-recipe-description" id="new-recipe-description"
        v-model="newRecipe.recipeInstructions" />
        <label for="image">Recipe Image URL:</label>
      <input type="text" name="new-recipe-image" id="new-recipe-image"
        v-model="newRecipe.recipeImage" />
      <button v-on:click.prevent="addNewRecipe">Add +</button>
    </div>
    <button class="btn-create-recipe" type="button" @click="showRecipeForm = !showRecipeForm">
      Create New Recipe</button>
    <div class="actions">
      <button class="btn-submit" type="submit">Save Meal</button>
      <button class="btn-cancel" type="button" @click="cancelForm">Return</button>
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
      },
      showRecipeForm: false,
      newRecipe: {
        recipeId: 0,
        userId: 0,
        recipeName: "",
        recipeInstructions: "",
        ingredientsList: [],
        recipeImage: ""
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

    addRecipe() {
      this.addedRecipes.push(this.newRecipe);
      this.newRecipe = {};
    },

    addRecipesToMeal() {
      this.editMeal.recipeList = this.addedRecipes;
      this.addedRecipes.forEach(addedRecipe => {
        console.log(addedRecipe, this.editMeal);
        mealService
          .addRecipeToMeal(this.editMeal.mealId, addedRecipe)
          .then((response) => {
            this.addedRecipes = [];
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
    addNewRecipe() {
      console.log("new recipe",this.newRecipe)
      recipeService
        .createRecipe(this.newRecipe)
        .then((response) => {
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
    createNewMeal() {
      if (this.editMeal.mealName) {
        mealService
          .createMeal(this.editMeal)
          .then((response) => {
            this.editMeal = response.data;
            this.addRecipesToMeal();
            this.cancelForm();
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
    
<style>
label {
  display: block;
  margin-bottom: 8px;
  font-size: large;
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
  padding-left: 4rem;
}

input[type="text"] {
  width: 60%;
  padding: 8px;
  margin-bottom: 12px;
  box-sizing: border-box;
  border-radius: 1rem;
  background-color: rgb(255, 237, 202);
  ;
  text-align: center;
}

select {
  width: 50%;
  padding: 8px;
  margin-bottom: 12px;
  box-sizing: border-box;
  border-radius: 1rem;
  background-color: rgb(255, 237, 202);
  text-align: center;
}

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

.btn-cancel {
  background-color: #ccc;
}

/* Additional styles as needed */
.actions {
  margin-top: 20px;
}
</style>