<template>
    <div class="list-groceries">
        <h1>Grocery List</h1>
        <section v-if="isLoading == false" class="container">
            <ingredient v-for="ingredient in groceries" 
            v-bind:key="ingredient.ingredientId" v-bind:item="ingredient" />
        </section>
        <div class="actions">
            <button class="btn-cancel" type="button" @click="cancel">Cancel</button>
        </div>
    </div>
</template>

<script>
import mealPlanService from '../services/MealPlanService.js';
import Ingredient from '../components/Ingredient.vue';

export default {
    name: "GroceriesView",
    components: {
        Ingredient
    },
    prop: {
        params: [
            'mealPlanId'
        ]
    },
    data() {
        return {
            isLoading: true,
            ingredients: [],
            mealPlanId: 0
        };
    },
    methods: {
        loadGroceries() {
            mealPlanService
                .getGroceries(this.mealPlanId)
                .then((response) => {
                    console.log(response);
                    this.groceries = response.data;
                    this.isLoading = false;
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error loading groceries: ", error.response.status);
                    } else if (error.request) {
                        console.log(
                            "Error loading groceries: unable to communicate to server"
                        );
                    } else {
                        console.log("Error loading groceries: make request");
                    }
                });
        },
        cancel() {
            this.$router.back();
        }
    },

    created() {
        this.mealPlanId = this.$route.params.mealPlanId;
        this.loadGroceries();
    },
};
</script>
  
  
<style scoped>
section.mealplan {
    background-color: rgb(255, 207, 111);
    font-family: Verdana, Geneva, Tahoma, sans-serif;
    border-radius: 0.5rem;
    margin-bottom: 1.25rem;
    width: 90%;
    padding: 2%;
    text-align: center;
    border-width: .2rem;
    padding-right: 20%;

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
  