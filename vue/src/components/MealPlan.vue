<template>
    <section class="mealplan">
        <h3>Name: {{ mealplan.mealPlanName }}</h3>
        <p>Description: {{ mealplan.mealPlanDescription }}</p>
        <section class="container">
            <h4>Meals:</h4>
            <meal v-for="meal in meals" v-bind:key="meal.id" v-bind:item="meal" />
        </section>
        <div class="button-container">
            <button class="save-mealplan" v-on:click.prevent="saveMealPlan" v-if="!showDetails">
                {{ feedback }}
            </button>
            <button class="view-mealplan-details" v-on:click="this.$router.push('/mealplan/' + mealplan.mealPlanId)"
                v-if="showDetails">
                View Meal Plan Details
            </button>
            <button class="remove-mealplan" v-on:click.prevent="removeMealPlan" v-if="showDetails">
                Delete Meal Plan
            </button>
            <button class="edit-mealplan" v-on:click="this.$router.push('/mealplan/' + mealplan.mealPlanId + '/edit')"
                v-if="showEdit">
                Edit Meal PLan
            </button>
        </div>
        <p></p>
    </section>
</template>
  
<script>
import meal from "../components/Meal.vue";
import mealPlanService from "../services/MealPlanService";

export default {
    name: "mealplan",
    props: ["item"],
    components: {
        meal,
    },
    data() {
        return {
            mealplan: {},
            meals: [],
            showDetails: this.$route.name == "mealplan",
            showEdit: this.$route.name == "mealPlanDetails",
            feedback: "Create Meal Plan",
        };
    },
    methods: {
        loadMeals() {
            mealPlanService
                .listMealsFromPlan(this.item.mealPlanId)
                .then((response) => {
                    this.meals = response.data;
                })
                .catch((error) => {
                    if (error.response) {
                        console.log(
                            "Error loading meals: ",
                            error.response.status
                        );
                    } else if (error.request) {
                        console.log(
                            "Error loading meals: unable to communicate to server"
                        );
                    } else {
                        console.log("Error loading meals: make request");
                    }
                });
        },

        saveMealPlan() {
            mealPlanService
                .addMealToPlan(this.meal)
                .then((response) => {
                    console.log(response);
                    this.$router.push({ name: "mealplan" });
                    this.buttonClick();
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error saving mealplan: ", error.response.status);
                    } else if (error.request) {
                        console.log("Error saving mealplan: unable to communicate to server");
                    } else {
                        console.log("Error saving mealplan: make request");
                    }
                });
        },

        removeMealPlan() {
            mealPlanService
                .removeMealPlan(this.mealplan.mealPlanId)
                .then((response) => {
                    console.log(response);
                    location.reload();
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error removing mealplan: ", error.response.status);
                    } else if (error.request) {
                        console.log(
                            "Error removing mealplan: unable to communicate to server"
                        );
                    } else {
                        console.log("Error removing mealplan: make request");
                    }
                });
        },

        buttonClick() {
            this.feedback = "Created";
        },
    },

    created() {
        console.log('reached mealplan', this.item);
        this.mealplan = this.item;
        this.loadMeals();
    },
};
</script>
  
<style scoped>
.list-all-mealplans {
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

.mealplan {
    text-align: center;
    background-color: rgb(225, 203, 164);
    border-radius: 0.625rem;
    /* Rounded corners for recipe cards */
    box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.1);
    margin-bottom: 1.25rem;
    padding: 2%;
}



.add-mealplan-button {
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
  