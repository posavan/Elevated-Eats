<template>
  <form v-on:submit.prevent="createNewIngredient">
    <div>
      <!--qty text box-->
      <label for="type">Quantity: </label>
      <input type="text" name="quantity" id="quantity" v-model="editIngredient.quantity" />
    </div>
    <div>
      <label for="name">Name: </label>
      <input type="text" name="name" id="name" v-model="editIngredient.ingredientName" />
    </div>
    <div class="actions">
<<<<<<< HEAD
      <button class="btn-continue" type="submit">Save Ingredient</button>
      <button class="btn-submit" type="submit">Save and Continue Adding</button>
      <button class="btn-cancel" type="button" v-on:click="cancelForm">Cancel</button>
=======
      <button class="btn-submit" type="submit">Add and Continue</button>
      <button class="btn-cancel" type="button" v-on:click="cancelForm">Return</button>
>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
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
            this.$router.push({ name: "favorites" });
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
