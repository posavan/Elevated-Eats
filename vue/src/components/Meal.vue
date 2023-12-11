<!-- placeholder code -->
<template>
    <section class="meal">
        <h3>Name: {{ meal.mealName }}</h3>
        <section class="container">
            <h4>Recipes:</h4>
            <recipe v-for="recipe in recipes" v-bind:key="recipe.id" v-bind:item="recipe" />
        </section>
        <p>Description: {{ meal.mealDescription }}</p>
        <div class="button-container">
            <button class="save-meal" v-on:click.prevent="saveMeal" v-if="showDetails">
                {{ feedback }}
            </button>
            <button class="view-meal-details" v-on:click="this.$router.push('/meal/' + meal.mealId)" v-if="!showEdit">
                View Meal Details
            </button>
            <button class="remove-meal" v-on:click.prevent="removeMeal" v-if="!showDetails">
                Delete Meal
            </button>
            <button class="edit-meal" v-on:click="this.$router.push('/meal/' + meal.mealId + '/edit')" v-if="showEdit">
                Edit Meal
            </button>
        </div>
        <p></p>
    </section>
</template>
<script>
import recipe from "../components/Recipe.vue";
import mealService from "../services/MealService.js";
import recipeService from "../services/RecipeService";

export default {
    name: "meal",
    props: ["item"],
    components: {
        recipe,
    },
    data() {
        return {
            meal: {},
            recipes: [],
            showDetails: this.$route.name == "meal",
            showEdit: this.$route.name == "mealDetails",
            mealId: 0,
            feedback: "Create Meal",
        };
    },
    methods: {
        loadRecipes() {
            recipeService
                .listUserRecipes()
                .then((response) => {
                    this.recipes = response.data;
                })
                .catch((error) => {
                    if (error.response) {
                        console.log(
                            "Error loading recipes: ", error.response.status);
                    } else if (error.request) {
                        console.log(
                            "Error loading recipes: unable to communicate to server"
                        );
                    } else {
                        console.log("Error loading recipes: make request");
                    }
                });
        },

        saveMeal() {
            mealService
                .createMeal(this.meal)
                .then((response) => {
                    console.log(response);
                    this.$router.push({ name: "meal" });
                    this.buttonClick();
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error saving meal: ", error.response.status);
                    } else if (error.request) {
                        console.log("Error saving meal: unable to communicate to server");
                    } else {
                        console.log("Error saving meal: make request");
                    }
                });
        },

        removeMeal() {
            mealService
                .deleteMeal(this.meal.mealId)
                .then((response) => {
                    console.log(response);
                    location.reload();
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error removing meal: ", error.response.status);
                    } else if (error.request) {
                        console.log(
                            "Error removing meal: unable to communicate to server"
                        );
                    } else {
                        console.log("Error removing meal: make request");
                    }
                });
        },

        buttonClick() {
            this.feedback = "Created";
        },
    },

    created() {
        this.meal = this.item;
        this.mealId = this.$route.params.mealId;
        this.loadRecipes();
    },
};
</script>
<style scoped>
.list-all-meals {
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

.meal {
    text-align: center;
    background-color: rgb(225, 203, 164);
    border-radius: 0.625rem;
    /* Rounded corners for recipe cards */
    box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.1);
    margin-bottom: 1.25rem;
    padding: 2%;
}



.add-meal-button {
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
<!-- end placeholder -->
