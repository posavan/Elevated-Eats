<template>
    <div class="list-mealplans">
        <h1>My Meal Plans</h1>
        <section class="container">
            <mealplan v-for="mealplan in mealplans" v-bind:key="mealplan.mealPlanId" v-bind:item="mealplan" />

        </section>
        <div class="create-mealplan">
            <button v-on:click="$router.push({ name: 'addMealPlan' })">Create Meal Plan</button>
        </div>
    </div>
</template>

<script>
import mealPlanService from '../services/MealPlanService.js';
import mealplan from '../components/MealPlan.vue';

export default {
    name: "MealPlanView",
    components: {
        mealplan,
    },
    data() {
        return {
            mealplans: [],
            newMealPlan: {},
            mealPlanId: 0
        };
    },
    methods: {
        loadMealPlans() {
            mealPlanService
                .list()
                .then((response) => {
                    console.log(response);
                    this.mealplans = response.data;
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error loading mealplans: ", error.response.status);
                    } else if (error.request) {
                        console.log(
                            "Error loading mealplans: unable to communicate to server"
                        );
                    } else {
                        console.log("Error loading mealplan: make request");
                    }
                });
        },
    },

    created() {
        this.loadMealPlans();
    },
};
</script>
  
  
<style scoped>
section.mealplan {
    background-color: rgb(171, 247, 201);
    font-family: Verdana, Geneva, Tahoma, sans-serif;
    border-radius: 0.5rem;
    margin-bottom: 1.25rem;
    width: 90%;
    padding: 2%;
    text-align: center;
}

.container {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
    gap: 10px;
}

h1 {
    text-align: center;
}

form {
    text-align: center;
}
.add-mealplan {
    display: block;
    color: white;
    text-align: center;
    padding: 14px 16px;
    background-color: rgb(169, 235, 161);
    border-radius: 50px;
    margin-left: 40%;
    margin-right: 40%;
    border-top: none;
    border-left: none;
    border-right: none;
    margin-bottom: 0.9%;
    text-decoration: none;
    font-family: sans-serif;
    font-size: 16px;
}

</style>
  