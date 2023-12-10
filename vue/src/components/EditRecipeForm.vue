<template>
  <form v-on:submit.prevent="updateRecipe">
    <div>
<<<<<<< HEAD
      <label for="type">Edit Ingredient Name: </label>
=======
      <!--add ingredients button-->
      <label for="type">Edit Ingredients: </label>
>>>>>>> f0dcfada6de9610c5e3fbbd38198d7ea7ccfb83f
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
<<<<<<< HEAD
        recipeId: this.recipeId,
        recipeName: this.recipeName,
        recipeInstructions: this.recipeInstructions
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
=======
        recipeId: this.recipe.recipeId,
        recipeName: this.recipe.recipeName,
        recipeInstructions: this.recipe.recipeInstructions,
      },
    };
>>>>>>> f0dcfada6de9610c5e3fbbd38198d7ea7ccfb83f
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