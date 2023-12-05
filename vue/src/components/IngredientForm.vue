<template>
  <form v-on:submit.prevent="createNewIngredient">
    <div>
      <label for="name">Name: </label>
      <input
        type="text"
        name="name"
        id="name"
        v-model="editIngredient.ingredientName"
      />
    </div>
    <div>
      <label for="type">Quantity: </label>
      <input
        type="text"
        name="quantity"
        id="quantity"
        v-model="editIngredient.quantity"
      />
    </div>
    <div>
      <label for="type">Calories: </label>
      <input
        type="number"
        name="calories"
        id="calories"
        v-model="editIngredient.calories"
      />
    </div>
    <div class="actions">
      <button class="btn-submit" type="submit">Submit</button>
      <button class="btn-cancel" type="button" v-on:click="cancelForm">
        Cancel
      </button>
    </div>
  </form>
</template>

<script>
import ingredientService from "../services/IngredientService.js";

export default {
  props: {
    ingredient: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      editIngredient: {
        ingredientId: this.ingredient.ingredientId,
        ingredientName: this.ingredient.ingredientName,
        quantity: this.ingredient.quantity,
        calories: this.ingredient.calories,
      },
      recipeId: this.ingredient.recipeId,
    };
  },

  methods: {
    createNewIngredient() {
      if (this.editIngredient.ingredientName) {
        ingredientService
          .createIngredient(this.editIngredient)
          .then(() => {
            this.editIngredient = {};
            this.$router.push({ name: "ListRecipesView" });
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error adding Ingredient: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error adding Ingredient: unable to communicate to server"
              );
            } else {
              console.log("Error adding Ingredient: make request");
            }
          });
      }
    },
    cancelForm() {
      this.$router.back();
    },
  },
};
</script>
