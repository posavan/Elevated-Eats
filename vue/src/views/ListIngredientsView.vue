<template>
  <div class="list-ingredients">
    <h1>Ingredients</h1>
    <button
      class="btn-add"
      v-on:click="$router.push({ name: 'AddIngredientView' })"
    >Add Ingredient
    </button>
    <section class="container">
      <ingredient
        v-for="ingredient in ingredients"
        v-bind:key="ingredient.id"
        v-bind:item="ingredient"
      />
    </section>
    <!-- 
    <button v-show="!showForm" v-on:click="showForm = true">
      Add Ingredient
    </button>

    <form v-on:submit.prevent="createNewIngredient" v-show="showForm">
      <div>
        <label for="name">Name: </label>
        <input type="text" name="name" id="name" v-model="newIngredient.ingredientName" />
      </div>
      <div>
        <label for="type">Calories: </label>
        <input type="number" name="calories" id="calories" v-model="newIngredient.calories" />
      </div>
      <button type="submit">Save Ingredient</button>
    </form> -->
  </div>
</template>

<script>
import Ingredient from "../components/Ingredient.vue";
import ingredientService from "../services/IngredientService.js";

export default {
  components: { Ingredient },
  name: "ListIngredientsView",
  data() {
    return {
      ingredients: [],
      showForm: false,
      newIngredient: {},
    };
  },
  // computed: {
  //   currentIngredients() {
  //     return this.$store.state.ingredients;
  //   },
  // // },
  methods: {
    //   createNewIngredient() {
    //     if (this.newIngredient.ingredientName) {
    //       ingredientService
    //         .createIngredient(this.newIngredient)
    //         .then(() => {
    //           this.newIngredient = {};
    //           this.showForm = false;
    //           this.loadIngredients();
    //         })
    //         .catch((error) => {
    //           if (error.response) {
    //             // error.response exists
    //             // Request was made, but response has error status (4xx or 5xx)
    //             console.log("Error adding Ingredient: ", error.response.status);
    //           } else if (error.request) {
    //             // There is no error.response, but error.request exists
    //             // Request was made, but no response was received
    //             console.log(
    //               "Error adding Ingredient: unable to communicate to server"
    //             );
    //           } else {
    //             // Neither error.response and error.request exist
    //             // Request was *not* made
    //             console.log("Error adding Ingredient: make request");
    //           }
    //         });
    //     }
    //   },

    loadIngredients() {
      ingredientService
        .list()
        .then((response) => {
          console.log("Reached created in ListIngredientsView.vue");
          console.log(response);
          this.ingredients = response.data;
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error loading ingredients: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log(
              "Error loading ingredients: unable to communicate to server"
            );
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error loading ingredients: make request");
          }
        });
    },
  },
  created() {
    this.loadIngredients();
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
section.ingredient {
  background-color: antiquewhite;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}
</style>
