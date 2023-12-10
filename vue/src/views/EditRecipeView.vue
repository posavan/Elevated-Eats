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
<<<<<<< HEAD
=======
<<<<<<< HEAD

=======
<<<<<<< HEAD
=======
=======
>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
>>>>>>> f0dcfada6de9610c5e3fbbd38198d7ea7ccfb83f
>>>>>>> 00cae56a8e2a28d52d2b0d563d6b7f166c5c0684
>>>>>>> 1e860c9f4ab4e25768d00ea4ed1b8e33a3b6802e
