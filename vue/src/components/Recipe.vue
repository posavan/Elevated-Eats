<template>
  <section class="recipe">
    <h3>Name: {{ recipe.recipeName }}</h3>
    <section v-if="!hide" class="container">
      <h4>Ingredients:</h4>
      <ingredient
        v-for="ingredient in ingredients"
        v-bind:key="ingredient.id"
        v-bind:item="ingredient"
      />
    </section>
    <p>Instructions: {{ recipe.recipeInstructions }}</p>
    <div class="button-container">
      <button class="save-recipe" v-on:click.prevent="saveRecipe" v-if="hide">
        {{ feedback }}
      </button>

      <button
        class="remove-recipe"
        v-on:click.prevent="removeRecipe"
        v-if="!hide"
      >
        Delete Recipe
      </button>

      <button
        class="view-recipe-details"
        v-on:click="$router.push('/recipe/favorites/' + recipeId)"
        v-if="showDetails"
      >
        View Recipe Details
      </button>
      <button
        class="edit-recipe"
        v-on:click="$router.push('/recipe/favorites/' + recipeId + '/edit')"
        v-if="showEdit"
      >
        Edit Recipe
      </button>
    </div>
    <p></p>
  </section>
</template>

<script>
import ingredient from "../components/Ingredient.vue";
import recipeService from "../services/RecipeService.js";

export default {
  name: "recipe",
  props: ["item"],
  components: {
    ingredient,
  },
  data() {
    return {
      recipe: {},
      ingredients: [],
      hide: this.$route.name == "recipe",
      showDetails: this.$route.name == "favorites",
      showEdit: this.$route.name == "userRecipeDetails",
      recipeId: 0,
      feedback: "Add Recipe To Favorites",
    };
  },
  methods: {
    loadRecipeIngredients() {
      recipeService
        .listIngredients(this.recipe.recipeId)
        .then((response) => {
          console.log("successful recipe/loadRecipeIngredients");
          this.ingredients = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log(
              "Error loading recipe ingredients: ",
              error.response.status
            );
          } else if (error.request) {
            console.log(
              "Error loading ingredients: unable to communicate to server"
            );
          } else {
            console.log("Error loading ingredients: make request");
          }
        });
    },
    // addIngredientToRecipe() {
    //   this.newRecipe.ingredientList.add(this.newIngredient);
    //   this.newIngredient = {};
    // },

    saveRecipe() {
      recipeService
        .addRecipeToUser(this.recipe)
        .then((response) => {
          console.log(response);
          this.$router.push({ name: "recipe" });
          this.buttonClick();
          //location.reload();
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error saving recipe: ", error.response.status);
          } else if (error.request) {
            console.log("Error saving recipe: unable to communicate to server");
          } else {
            console.log("Error saving recipe: make request");
          }
        });
    },

    removeRecipe() {
      recipeService
        .removeRecipeFromUser(this.recipe.recipeId)
        .then((response) => {
          console.log(response);
          //this.$router.push({name: 'favorites' });
          location.reload();
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error removing recipe: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error removing recipe: unable to communicate to server"
            );
          } else {
            console.log("Error removing recipe: make request");
          }
        });
    },

    buttonClick() {
      this.feedback = "Added";
      // setTimeout(resetMessage, 3000);
      // function resetMessage() {
      //   this.feedback = "Add Recipe To Favorites";
      // }
    },
  },

  created() {
    this.recipe = this.item;
    this.recipeId = this.$route.params.recipeId;
    this.loadRecipeIngredients();
  },
};
</script>

<style scoped>
.list-all-recipes {
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  text-align: center;
  /* Center text for better appearance */
  padding-right: 20%;
}

h1 {
  text-align: center;
}

section {
  text-align: center;
  justify-content: space-between;
}

.container {
  
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
  gap: 1.25rem;
}

.recipe {
  text-align: center;
  background-color: rgb(225, 203, 164);
  border-radius: 0.625rem;
  /* Rounded corners for recipe cards */
  box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.1);
  margin-bottom: 1.25rem;
  padding: 2%;
}

.add-recipe-button {
  margin-top: 1.25rem;
  align-items: center;
}

form {
  display: flex;
  flex-direction: column;
  max-width: 18.75rem;
  margin: auto;
  margin-top: 1.25rem;
  /* Add spacing between the button and the form */
}

form div {
  margin-bottom: 0.625rem;
}

label {
  display: block;
  margin-bottom: 0.3125rem;
}

input {
  width: 100%;
  padding: 0.625rem;
  box-sizing: border-box;
}


button {
  padding: 1.25rem;
  background-color: brown;
  color: #fff;
  border: none;
  border-radius: 0.25rem;
  cursor: pointer;
  margin-right: 1.25rem;
}

button {
  padding: 1.25rem;
  background-color: #4caf50;
  justify-content: space-between;
  color: #fff;
  border: none;
  border-radius: 0.25rem;
  cursor: pointer;
  margin-right: 1.25rem;
}

button:hover {
  opacity: 0.8;
}
</style>
