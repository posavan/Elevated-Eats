<template>
  <h1>Edit Recipe</h1>
  <edit-recipe-form v-bind:recipe="recipe" />

<!--needs add ingredient button that allows user to add an ingredient not already part of this recipe-->
<!--add ingredient button brings up a list of ingredients from ListIngredientsView-->

</template>

<script>
import recipeService from "../services/RecipeService";
import EditRecipeForm from "../components/EditRecipeForm.vue";

export default {
  components: {
    EditRecipeForm,
  },
  data() {
    return {
      editRecipe: {
        name: "",
        recipeId: Number(this.recipeId),
      },
    };
  },
  methods: {
    updateRecipe() {
      recipeService
        .updateRecipe(this.editRecipe.recipeId, this.editRecipe)
        .then((response) => {
          this.$router.push({
            name: "EditRecipeView",
            params: { id: this.editRecipe.recipeId },
          });
        })
        .catch((error) => {});
    },
  },
};
</script>

