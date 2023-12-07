<template>
  <h1>Add New Ingredient</h1>
  <ingredient-form v-bind:ingredient="ingredient" />
</template>

<script>
import IngredientForm from "../components/IngredientForm.vue";

export default {
  components: {
    IngredientForm,
  },
  data() {
    return {
      ingredient: {
        id: 0,
        name: "",
        quantity: "1 ea",
        recipeId: Number(this.$route.params.recipeId)
      },
    };
  },
};
</script>
