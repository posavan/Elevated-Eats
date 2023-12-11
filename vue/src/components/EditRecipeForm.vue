<template>
  <form v-on:submit.prevent="updateRecipe">
    <div v-for="ingredient in Array.from(editRecipe.ingredientList)" :key="ingredient.ingredientId">

      <label for="type">Edit Ingredient Name: </label>
      <input type="text" name="edit-ingredient-name" id="edit-ingredient-name" v-model="ingredient.ingredientName" />
    
      <label for="type">Edit Ingredient Quantity: </label>
      <input type="text" name="edit-ingredient-quantity" id="edit-ingredient-quantity" v-model="ingredient.quantity" />
    </div>

    <div>
      <label for="name">Edit Instructions: </label>
      <input type="text" name="edit-instructions" id="edit-instructions" v-model="editRecipe.recipeInstructions" />
    </div>

    <div class="actions">
      <button class="btn-continue" type="submit">Save Recipe</button> <!--Needs to return to prev-->
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
  props: ['recipe'],
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
    updateRecipe(){
      recipeService
      .updateRecipe(this.editRecipe.recipeId, this.editRecipe)
        .then(() => {
          this.$router.push({ name: "userRecipeDetails", params: {id: this.editRecipe.recipeId} });
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
    cancelForm(){
      
        this.$router.back();
    
    }
  },

  created() {
    this.editRecipe = this.recipe
    console.log('logging editRecipeFormData', this.editRecipe)

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