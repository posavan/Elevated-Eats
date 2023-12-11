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
<<<<<<< HEAD
  components: {
    EditRecipeForm,
=======
  components: EditRecipeForm,
  prop: {
    recipe: {
      type: Object,
      required: true,

    },
>>>>>>> 84a5cadec79dec09593ce558d685e6f8dcb2d370
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
>>>>>>> 84a5cadec79dec09593ce558d685e6f8dcb2d370
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
<<<<<<< HEAD
        .catch((error) => {});
=======
        .catch((error) => { })
      },
      deleteRecipe() {
        if (
          confirm(
            "Are you sure you want to delete this recipe and all associated ingredients? This action cannot be undone."
          )
        ) {
          recipeService.removeRecipeFromUser(this.recipe.recipeId)
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
>>>>>>> 84a5cadec79dec09593ce558d685e6f8dcb2d370
    },
  };
</script>

