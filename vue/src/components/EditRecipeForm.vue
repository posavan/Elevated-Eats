<template>
  <form v-on:submit.prevent="editRecipe">
    <div>
      <label for="type">Edit Ingredients: </label>
      <input type="text" name="edit-ingredient-name" id="edit-ingredient-name" v-model="editRecipe.ingredientName" />
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
        recipeInstructions: this.recipe.recipeInstructions
      },
    };
  },

  methods: {

  }
};
</script>
