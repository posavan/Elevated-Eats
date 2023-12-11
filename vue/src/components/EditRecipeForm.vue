<template>
  <form v-on:submit.prevent="updateRecipe">
    <div>
      <!--add ingredients button-->
      <label for="type">Edit Ingredients: </label>
      <input type="text" name="edit-ingredient-name" id="edit-ingredient-name" v-model="editRecipe.ingredientName" />
    </div>

    <div>
      <label for="type">Edit Ingredient Quantity: </label>
      <input type="text" name="edit-ingredient-quantity" id="edit-ingredient-quantity" v-model="editRecipe.quantity" />
    </div>

    <div>
      <label for="name">Edit Instructions: </label>
      <input type="text" name="edit-instructions" id="edit-instructions" v-model="editRecipe.recipeInstructions" />
    </div>

    <div class="actions">
      <button class="btn-continue" type="submit">Save Recipe</button><!--Needs to return to prev-->
      <button class="btn-submit" type="submit">
        Save and Continue Editing
      </button>
      <button class="btn-cancel" type="button" v-on:click="cancelForm">
        Cancel
      </button>
    </div>
  </form>
</template>

<script>
import recipeService from "../services/RecipeService.js";

export default {
  props: {
    recipe: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      editRecipe: {
        recipeId: this.recipe.recipeId,
        recipeName: this.recipe.recipeName,
        recipeInstructions: this.recipe.recipeInstructions,
      },
    };
  }
}
</script>

<style scoped>
h1 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
  background-color: rgb(206, 182, 236);
  border-radius: 50px;
}

div {
  background-color: rgb(14, 136, 71);
  font-family: "Gill Sans", "Gill Sans MT", Calibri, "Trebuchet MS", sans-serif;
  font-size: large;
  text-align: center;
  border-radius: 50px;
  padding: auto;
}
</style>