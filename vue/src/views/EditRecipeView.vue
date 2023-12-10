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
=======
<<<<<<< HEAD
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
>>>>>>> f0dcfada6de9610c5e3fbbd38198d7ea7ccfb83f
  },
  data() {
    return {
      editRecipe: {
<<<<<<< HEAD
        name: "",
        recipeId: Number(this.recipeId),
=======
        recipeId: this.recipeId,
        recipeName: this.recipeName,
        recipeInstructions: this.recipeInstructions,
>>>>>>> f0dcfada6de9610c5e3fbbd38198d7ea7ccfb83f
      },
    };
  },
  methods: {
<<<<<<< HEAD
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
=======
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
>>>>>>> f0dcfada6de9610c5e3fbbd38198d7ea7ccfb83f
    },
  },
};
</script>
<<<<<<< HEAD

=======
<<<<<<< HEAD
=======
=======
>>>>>>> e4978dacac6c0dc4bfb9bff593d5b6688eb3cc0f
>>>>>>> f0dcfada6de9610c5e3fbbd38198d7ea7ccfb83f
>>>>>>> 00cae56a8e2a28d52d2b0d563d6b7f166c5c0684
