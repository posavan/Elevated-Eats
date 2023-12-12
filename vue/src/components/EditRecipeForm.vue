<template>
  <form v-on:submit.prevent="updateRecipe">
    <div
      v-for="ingredient in Array.from(editRecipe.ingredientList)"
      :key="ingredient.ingredientId"
    >
      <label for="type">Edit Ingredient Name: </label>
      <input
        type="text"
        name="edit-ingredient-name"
        id="edit-ingredient-name"
        v-model="ingredient.ingredientName"
      />

      <label for="type">Edit Ingredient Quantity: </label>
      <input
        type="text"
        name="edit-ingredient-quantity"
        id="edit-ingredient-quantity"
        v-model="ingredient.quantity"
      />
    </div>

    <ingredient
      v-for="ingredient in addedIngredients"
      v-bind:key="ingredient.ingredientId"
      v-bind:item="ingredient"
    >
    </ingredient>

    <div v-if="showAddIngredientForm">
      <label for="type">New Ingredient Name: </label>
      <input
        type="text"
        name="new-ingredient-name"
        id="new-ingredient-name"
        v-model="newIngredient.ingredientName"
      />

      <label for="type">New Ingredient Quantity: </label>
      <input
        type="text"
        name="new-ingredient-quantity"
        id="new-ingredient-quantity"
        v-model="newIngredient.quantity"
      />
      <button v-on:click.prevent="addNewIngredient">Add +</button>
    </div>

    <div>
      <label for="name">Edit Instructions: </label>
      <input
        type="text"
        name="edit-instructions"
        id="edit-instructions"
        v-model="editRecipe.recipeInstructions"
      />
    </div>

    <label for="ingredients">Ingredients:</label>
    <select v-model="selected">
      <option
        v-for="ingredient in ingredients"
        :key="ingredient.ingredientId"
        :value="ingredient"
      >
        {{ ingredient.ingredientName }}
      </option>
    </select>

    <button type="button" v-on:click="addIngredientsToRecipe">
      Add Ingredient
    </button>

    <button type="button" v-on:click="showAddIngredientForm = true">
      Add New Ingredient
    </button>

    <div class="actions">
      <button class="btn-submit" type="submit">Save Recipe</button>
      <button class="btn-cancel" type="button" v-on:click="cancelForm">
        Cancel
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
      },
      showAddIngredientForm: false,
      newIngredient: {
        ingredientName: "",
        quantity: "",
      },
      
    };
  },

  methods: {
    loadIngredients() {
      ingredientService
        .list()
        .then((response) => {
          console.log("reached loadIngredients", response.data);
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

    addIngredientsToRecipe() {
      if (this.selected) {
        this.addedIngredients.push({
          ingredientName: this.selected.ingredientName,
          quantity: this.selected.quantity,
        });
        this.selected = {};
      }
    },

    updateRecipe() {
      console.log('UpdatingRecipe...');
      console.log('editRecipe', this.editRecipe);
      this.editRecipe.ingredientList = this.addedIngredients;
      recipeService
        .updateRecipe(this.editRecipe)
        .then((response) => {
          console.log('server response:', response.data)
          this.$router.push({
            name: "userRecipeDetails",
            params: { id: this.editRecipe.recipeId },
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

    addNewIngredient() {
      this.addedIngredients.push({
        ingredientName: this.newIngredient.ingredientName,
        quantity: this.newIngredient.quantity,
      });
    },

    cancelForm() {
      this.$router.back();
    },
  },

  created() {
    this.editRecipe = this.recipe;
    console.log("logging editRecipeFormData", this.editRecipe);
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

input[type="text"] {
  width: 60%;
  padding: 8px;
  margin-bottom: 12px;
  box-sizing: border-box;
  border-radius: 1rem;
  background-color:rgb(255, 237, 202);
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
