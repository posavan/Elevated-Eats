<template>
  <h1 v-if="isLoading == false">Edit Recipe: {{editRecipe.recipeName}}</h1>
  <div>
  <EditRecipeForm v-bind:recipe="editRecipe" v-if="isLoading == false" />
</div>
  <!--needs add ingredient button that allows user to add an ingredient not already part of this recipe-->
  <!--add ingredient button brings up a list of ingredients from ListIngredientsView-->
</template>

<script>
import recipeService from "../services/RecipeService";
import EditRecipeForm from "../components/EditRecipeForm.vue";

export default {
  components: { EditRecipeForm },
  data() {
    return {
      isLoading: true,
      editRecipe: {
        recipeId: 0,
        recipeName: "",
        recipeInstructions: "",
        recipeImage: ""
      },
    };
  },
  methods: {
  
    updateRecipe() {
      console.log(this.editRecipe)
      recipeService
        .updateRecipe(this.editRecipe)
        .then((response) => {
          this.$router.push({
            name: "EditRecipeView",
            params: { id: this.editRecipe.recipeId },
          });
        })
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
    },

    created() {
      this.editRecipe.recipeId = this.$route.params.recipeId;
      console.log('logging editRecipe', this.editRecipe);
      this.editRecipe.recipeName = this.recipeName;
      this.editRecipe.recipeInstructions = this.recipeInstructions;
      this.editRecipe.recipeImage = this.recipeImage;
      recipeService.GetUserRecipeByRecipeId(this.editRecipe.recipeId)
      .then((response) => {
          console.log(response.data);
          this.editRecipe = response.data;
          this.isLoading = false;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading recipe: ", error.response.status);
          } else if (error.request) {
            console.log("Error loading recipe: unable to communicate to server");
          } else {
            console.log("Error loading recipe: make request");
          }
        });
    }
  };
</script>

<style scoped>
h1{
  align-items: center;
}
</style>