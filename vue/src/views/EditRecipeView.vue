<template>
  <h1>Edit Recipe</h1>
  <edit-recipe-form v-bind:recipe="recipe" />
</template>

<script>
import EditRecipeForm from "../components/EditRecipeForm.vue";
import RecipeService from "../services/RecipeService";

export default {
  components: EditRecipeForm,
  prop: {
    recipe: {
      type: Object,
      required: true,

    },
  },
  data() {
    return {
      editRecipe: {
        recipeId: this.recipeId,
        recipeName: this.recipeName,
        recipeInstructions: this.recipeInstructions,
      },
    };
  },
  methods: {
    deleteRecipe() {
      if (
        confirm(
          "Are you sure you want to delete this recipe and all associated ingredients? This action cannot be undone."
        )
      ) {
        RecipeService.removeRecipeFromUser(this.recipe.recipeId)
          .then((response) => {
            if (Response.status === 200) {
              this.$store.commit("SET_NOTIFICATION", {
                message: `Recipe ${this.recipe.recipeId} was successfully deleted.`,
                type: "success",
              });
              this.$router.push({
                name: "HomeView",
                params: { id: this.recipe.recipeId },
              });
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "deleting");
          });
      }
    },
  },
};
</script>

