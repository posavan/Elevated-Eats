<template>
  <h1>Recipe Library</h1>
  <div class="list-all-recipes">
    <section class="container">
      <recipe v-for="recipe in allRecipes" v-bind:key="recipe.id" v-bind:item="recipe">
        <button v-on:click="addRecipeToUser">Save Recipe To Favorites</button>
      </recipe>
    </section>
    <form v-on:submit.prevent="createNewRecipe" v-show="showForm">
      <div>
        <label for="name">Name: </label>
        <input type="text" name="name" id="name" v-model="newRecipe.recipeName" />
      </div>
      <!-- 
        <div>
          <h2>Ingredients: </h2>
          <label for="type">Quantity: </label>
          <input type="text" name="quantity" id="quantity" v-model="newIngredient.quantity" />
          <label for="type">Ingredient: </label>
          <input type="text" name="name" id="name" v-model="newIngredient.ingredientName" />
          <label for="type">Calories: </label>
          <input type="text" name="calories" id="calories" v-model="newIngredient.calories" />
          addIngredientToRecipe
          <recipe v-for="ingredient in newRecipe.ingredientList" v-bind:key="ingredient.id"
            v-bind:ingredient="ingredient" />
          
            <button class="btn-add" v-on:click="$router.push({ name: 'AddIngredientView', params: { recipeId: newRecipe.id } })">Add
            Ingredient</button>
        </div> -->
      <div>
        <label for="type">Instructions: </label>
        <input type="text" name="recipeInstructions" id="recipeInstructions" v-model="newRecipe.recipeInstructions" />
      </div>

      <button type="submit">Save Recipe</button>
    </form>
  </div>
</template>

<script>
import recipe from "../components/Recipe.vue";
import recipeService from "../services/RecipeService.js";

export default {
  components: { recipe },
  name: "ListAllRecipesView",
  data() {
    return {
      allRecipes: [],
      showForm: false,
      newRecipe: {},
      newIngredient: {},
      userId: 0,
    };
  },

  methods: {
    createNewRecipe() {
      if (this.newRecipe.recipeName) {
        recipeService
          .createRecipe(this.newRecipe)
          .then(() => {
            this.newRecipe = {};
            this.showForm = false;
            this.loadRecipes();
          })
          .catch((error) => {
            if (error.response) {
              console.log("Error adding recipe: ", error.response.status);
            } else if (error.request) {
              console.log(
                "Error adding recipe: unable to communicate to server"
              );
            } else {
              console.log("Error adding recipe: make request");
            }
          });
      }
    },

    loadRecipes() {
      recipeService
        .list()
        .then((response) => {
          console.log(response);
          this.allRecipes = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading recipes: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error loading recipes: unable to communicate to server"
            );
          } else {
            console.log("Error loading recipes: make request");
          }
        });
    },
    addIngredientToRecipe() {
      this.newRecipe.ingredientList.add(this.newIngredient);
      this.newIngredient = {};
    },
  },

  created() {
    this.loadRecipes();
  },
};
</script>

<style scoped>
section.recipe {
  background-color: rgb(220, 200, 163);
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  text-align: center;
  border-right: 20px;
}

.list-all-recipes {
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}

h1 {
  text-align: center;
}

.container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
  gap: 1.25rem;
}

form {
  display: flex;
  flex-direction: column;
  max-width: 18.75rem;
  margin: auto;
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
  color: wheat;
  border: none;
  border-radius: 0.25rem;
  cursor: pointer;
}

button:hover {
  opacity: 0.50;
}

.container recipe {
  background-color: rgb(224, 203, 163);
  border-radius: 0.5rem;
  margin-bottom: 1.25rem;
  width: 30%;
  padding: 2%;
}
</style>
