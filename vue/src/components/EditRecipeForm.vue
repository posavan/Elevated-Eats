<template>
  <form v-on:submit.prevent="updateRecipe">
    <div>
      <label for="name">Recipe: </label>
      <input type="text" name="edit-recipe-name" id="edit-recipe-name" v-model="editRecipe.recipeName" />
    </div>
    <div>
      <label for="instructions">Instructions: </label>
      <textarea placeholder="instructions" name="edit-instructions" id="edit-instructions" v-model="editRecipe.recipeInstructions" />
    </div>
    <div>
      <label for="image">Edit Image URL: </label>
      <input type="text" name="edit-image" id="edit-image" v-model="editRecipe.recipeImage" />
    </div>
    <h3>Ingredients:</h3>
    <div v-for="ingredient in Array.from(editRecipe.ingredientList)" :key="ingredient.ingredientId">
      <p>{{ ingredient.ingredientName }}</p>
      <button @click.prevent="removeIngredient(ingredient)">Remove</button>
    </div>

    <ingredient v-for="ingredient in addedIngredients" v-bind:key="ingredient.ingredientId" v-bind:item="ingredient">
    </ingredient>
    <div>
      <label for="ingredient">Ingredient:</label>
      <select v-model="newIngredient">
        <option v-for="ingredient in ingredients" :key="ingredient.ingredientId" :value="ingredient">
          {{ ingredient.ingredientName }}
        </option>
      </select>
      <label for="type">Quantity: </label>
      <input type="text" name="dropdown-quantity" id="dropdown-quantity" v-model="newIngredient.quantity" />
      <button type="button" v-on:click="addIngredient">
        Add Ingredient +
      </button>
    </div>
    <button type="button" v-on:click="buttonClick">
      {{ feedback }}
    </button>
    <div v-if="showAddIngredientForm">
      <label for="type">New Ingredient Name: </label>
      <input type="text" name="new-ingredient-name" id="new-ingredient-name" v-model="newIngredient.ingredientName" />
      <label for="type">New Ingredient Quantity: </label>
      <input type="text" name="new-ingredient-quantity" id="new-ingredient-quantity" v-model="newIngredient.quantity" />
      <button v-on:click.prevent="addNewIngredient">Create</button>
    </div>

    <div class="actions">
      <button class="btn-submit" type="submit">Save Recipe</button>
      <button class="btn-cancel" type="button" v-on:click="cancelForm">
        Return
      </button>
    </div>
  </form>
</template>

<script>
import recipeService from "../services/RecipeService.js";
import ingredientService from "../services/IngredientService.js";
import ingredient from "../components/Ingredient.vue";

export default {
  components: { ingredient },
  props: ["recipe"],
  data() {
    return {
      ingredients: [],
      addedIngredients: [],
      selected: {},
      editRecipe: {
        recipeId: this.recipe.recipeId,
        recipeName: this.recipe.recipeName,
        recipeInstructions: this.recipe.recipeInstructions,
        ingredientList: this.recipe.ingredientList,
        recipeImage: this.recipe.recipeImage,
      },
      showAddIngredientForm: false,
      newIngredient: {
        ingredientId: 0,
        ingredientName: "",
        quantity: "",
      },
      storeQuantity: 0,
      feedback: "Create New Ingredient"
    };
  },

  methods: {
    loadIngredients() {
      ingredientService
        .list()
        .then((response) => {
          this.ingredients = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading ingredients: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error loading ingredients: unable to communicate to server"
            );
          } else {
            console.log("Error loading ingredients: make request");
          }
        });
    },

    addNewIngredient() {
      this.storeQuantity = this.newIngredient.quantity;
      ingredientService
        .createIngredient(this.newIngredient)
        .then((response) => {
          this.newIngredient = response.data;
          this.newIngredient.quantity = this.storeQuantity;
          this.addedIngredients.push(
            this.newIngredient
          );
          this.newIngredient = {};
          console.log("Ingredient added successfully", response.data);
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error adding ingredient: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error adding ingredient: unable to communicate to server"
            );
          } else {
            console.log("Error adding ingredient: make request");
          }
        });
    },
    addIngredient() {
      this.addedIngredients.push(
        this.newIngredient
      );
      this.newIngredient = {};
    },
    removeIngredient(targetIngredient) {
      console.log(targetIngredient);
      recipeService
        .removeIngredientFromRecipe(this.editRecipe.recipeId, targetIngredient.ingredientId)
        .then((response) => {
          console.log("Ingredient removed successfully", response.data);
          location.reload();
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error adding ingredient: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error adding ingredient: unable to communicate to server"
            );
          } else {
            console.log("Error adding ingredient: make request");
          }
        });
    },

    addIngredientsToRecipe() {
      this.editRecipe.ingredientList = this.addedIngredients;
      recipeService
        .addIngredientsToRecipe(this.editRecipe)
        .then((response) => {
          console.log("Recipe edited successfully", response);
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error updating Recipe: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error updating Recipe: unable to communicate to server"
            );
          } else {
            console.log("Error updating Recipe: make request");
          }
        });
    },
    updateRecipe() {
      this.addIngredientsToRecipe();
      recipeService
        .updateRecipe(this.editRecipe)
        .then((response) => {
          console.log('server response:', response.data)
          this.$router.push({
            name: "userRecipeDetails",
            params: { recipeId: this.editRecipe.recipeId },
          });
          console.log("Recipe edited successfully");
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error updating Recipe: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error updating Recipe: unable to communicate to server"
            );
          } else {
            console.log("Error updating Recipe: make request");
          }
        });
    },
    buttonClick() {
      this.showAddIngredientForm = !this.showAddIngredientForm;
      if (this.showAddIngredientForm) {
        this.feedback = "Collapse";
      } else {
        this.feedback = "Create New Ingredient";
      }
    },
    cancelForm() {
      this.$router.back();
    },
  },

  created() {
    this.editRecipe = this.recipe;
    this.loadIngredients();
  },
};
</script>

<style scoped>
/* h1 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
  background-color: rgb(206, 182, 236);
  border-radius: 50px;
}

div {
  text-align: center;
  background-color: rgb(225, 203, 164);
  border-radius: 0.625rem;
  box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.1);
  margin-bottom: 1.25rem;
  padding: 2%;
}

input {
  padding: auto;
}

button {
  padding: 1%;
  background-color: #4caf50;
  justify-content: space-between;
  color: #fff;
  border: none;
  border-radius: 0.25rem;
  cursor: pointer;
  margin-right: 1.25rem;
}

button:hover {
  opacity: 0.8;
} */

.recipe-form {
  width: 50%;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 1.rem;
}

label {
  display: block;
  margin-bottom: 8px;
  font-size: large;
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
  padding-left: 4rem;
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
