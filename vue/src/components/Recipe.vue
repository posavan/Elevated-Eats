<template>
  <section class="recipe">
    <div class="image">
      <img v-bind:src="recipe.recipeImage" />
    </div>
    <h3>{{ recipe.recipeName }}</h3>

    <section v-if="inDetails" class="container">
      <div class="ingredients">Ingredients:</div>
      <ingredient
        v-for="ingredient in ingredients"
        v-bind:key="ingredient.ingredientId"
        v-bind:item="ingredient"
      />
    </section>

    <div class="instructions" v-if="inDetails">
      Instructions: {{ recipe.recipeInstructions }}
    </div>

    <div class="button-container">
      <button class="save-recipe" v-on:click.prevent="saveRecipe" v-if="show">
        {{ feedback }}
      </button>
      <button class="view-recipe-details" v-on:click="$router.push('/recipe/favorites/' + recipe.recipeId)"
        v-if="inMealDetails || inFavorites">
        View Recipe Details
      </button>
      <!-- <button class="view-public-details" v-on:click="$router.push('/recipe/public/' + recipe.recipeName)"
        v-if="show">
        View Recipe Details
      </button> -->
      <button class="edit-recipe" v-on:click="$router.push('/recipe/favorites/' + recipe.recipeId + '/edit')" v-if="inDetails">
        Edit Recipe
      </button>
      <button class="remove-recipe" v-on:click.prevent="removeRecipe" v-if="inFavorites || inEdit || inAdd || inMealDetails">
        Delete Recipe
      </button>
      <button class="btn-cancel" type="button" @click="cancel" v-if="inDetails">Return</button>
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
      recipeId: 0,
      feedback: "Copy Recipe To Favorites",
      inDetails: this.$route.name == "userRecipeDetails",
      inFavorites: this.$route.name == "favorites",
      inMealDetails: this.$route.name == "mealDetailsView",
      inAdd: this.$route.name == "createMeal",
      inEdit: this.$route.name == "EditMealView",
      show: this.$route.name == "recipe",
      inPublic: this.$route.name == "recipeDetails"
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
          this.cancel();
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
      this.feedback = "Copied";
      // setTimeout(resetMessage, 3000);
      // function resetMessage() {
      //   this.feedback = "Add Recipe To Favorites";
      // }
    },
    cancel() {
      this.$router.back();
    },
  },

  created() {
    this.recipe = this.item;
    this.recipeId = this.item.recipeId;
    this.loadRecipeIngredients();
  },
};
</script>

<style scoped>
.list-all-recipes {
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  text-align: right;
  /* Center text for better appearance */
  padding-right: 30%;
  position: center;
}
img {
  width: 100%;
  aspect-ratio: 3/2;
  object-fit: contain;
}
/* .recipe.image {
  image: url("/img/homeview.jpg");
} */

h1 {
  text-align: center;
}

section {
  text-align: center;
  justify-content: space-between;
  padding-bottom: 2px;
}

.container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
  gap: 1.25rem;
  text-align: center;
  padding: 1em;
  border-radius: 1rem;
  padding-bottom: 1rem;
}

.recipe {
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  text-align: center;
  border-right: 90%;
  margin-top: 1.9rem;
  border-radius: 0x;
  text-size-adjust: wrap;
  margin-top: 5%;
  
}

.ingredients {
  font-weight: bold;
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
  border-bottom: 1rem;
  /* Add spacing between the button and the form */
}

.instructions {
  text-align: center;
  padding-top: 3%;
  padding-left: 5%;
  padding-right: 5%;
  text-indent: 50px;
  line-height: 2;
  white-space: wrap;
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
/* 
button {
  display: inline-block;
  border-radius: 1.5rem;
  cursor: pointer;
  padding: 0.5rem 1.5rem;
  text-decoration: none;
  white-space: nowrap;
  text-transform: none;
  font-family: FuturaPT, helvetica, sans-serif;
  font-feature-settings: normal;
  font-style: normal;
  letter-spacing: normal;
  line-break: auto;
  line-height: 1.25em;
  font-size: 16px;
  font-weight: 500;
  overflow-wrap: normal;
  border-width: 0.2rem;
  margin-bottom: 1.9rem;
}

button:hover {
  border-style: dotted;
} */
button {
  font-size: 16px;
  font-weight: 200;
  letter-spacing: 1px;
  padding: 13px 20px 13px;
  outline: 0;
  border: 1px solid black;
  cursor: pointer;
  position: relative;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
}

button:after {
  content: "";
  background-color:rgb(254, 178, 108);
  width: 100%;
  z-index: -1;
  position: absolute;
  height: 100%;
  top: 7px;
  left: 7px;
  transition: 0.2s;
}

button:hover:after {
  top: 0px;
  left: 0px;
}

@media (min-width: 768px) {
  button{
    padding: 13px 50px 13px;
  }
}
</style>
