<template>
    <form v-on:submit.prevent="updateMealPlan">
        <div>
            <label for="name">Name: </label>
            <input type="text" name="name" id="name" v-model="editMealPlan.mealPlanName" />
        </div>
        <div>
            <label for="description">Description: </label>
            <textarea placeholder="description" name="description" id="description"
                v-model="editMealPlan.mealPlanDescription" />
        </div>
        <!-- display meals to be added -->
        <meal v-for="meal in this.editMealPlan.mealList" v-bind:key="meal.mealId" v-bind:item="meal" />
        <!-- <button class="btn-remove" type="button" @click="removeMeal">Remove Meal</button> -->
        <div>
            <!-- add each meal -->
            <h3>Meals:</h3>
            <select v-model="selected">
                <option v-for="meal in meals" v-bind:key="meal.id" :value="meal">
                    {{ meal.mealName }}
                </option>
            </select>
            <button class="btn-add-meal" type="button" @click="addMeal">Add Meal</button>
        </div>

        <button class="btn-create-meal" type="button" @click="this.$router.push({ name: 'createMeal' })">
            Create New Meal</button>
        <div class="actions">
            <button class="btn-submit" type="submit">Save Meal Plan</button>
            <button class="btn-cancel" type="button" @click="cancel" v-if="show">Return</button>
        </div>
    </form>
</template>
    
<script>
import mealPlanService from "../services/MealPlanService.js";
import mealService from "../services/MealService.js";
import meal from "../components/Meal.vue";

export default {
    components: {
        meal
    },
    props: {
        mealplan: {
            type: Object,
            required: true,
        },
    },
    data() {
        return {
            show: this.$route.name == "editMealPlan",
            showCreate: this.$route.name == "viewMealPlanDetails",
            showSave: this.$route.name == "addMealPlan",
            selected: {},
            meals: [],

            editMealPlan: {
                mealPlanId: this.mealplan.mealPlanId,
                mealPlanName: this.mealplan.mealPlanName,
                mealPlanDescription: this.mealplan.mealPlanDescription,
                mealList: this.mealplan.mealList
            },
        };
    },

    methods: {
        loadMeals() {
            mealPlanService
                .listMealsFromPlan(this.editMealPlan.mealPlanId)
                .then((response) => {
                    console.log("reached loadMeals", response.data);
                    this.editMealPlan.meals = response.data;
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
        loadAllMeals() {
            mealService
                .list()
                .then((response) => {
                    console.log("reached loadAllMeals", response.data);
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

        addMeal() {
            console.log("reached addMeal", this.editMealPlan, this.selected)
            mealPlanService
                .addMealToPlan(this.editMealPlan.mealPlanId, this.selected)
                .then((response) => {
                    console.log("reached loadMeals", response.data);
                    this.loadMeals();
                    location.reload();
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

        updateMealPlan() {
            mealPlanService
                .updateMealPlan(this.editMealPlan)
                .then((response) => {
                    console.log("reached loadMeals", response.data);
                    this.meals = response.data;
                    this.cancel();
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

        createNewMealPlan() {
            if (this.editMealPlan.mealPlanName) {
                mealPlanService
                    .createMealPlan(this.editMealPlan)
                    .then(() => {
                        this.editMealPlan = {};
                        this.$router.push({ name: "mealplan" });
                    })
                    .catch((error) => {
                        if (error.response) {
                            console.log("Error adding mealplan: ", error.response.status);
                        } else if (error.request) {
                            console.log(
                                "Error adding mealplan: unable to communicate to server"
                            );
                        } else {
                            console.log("Error adding mealplan: make request");
                        }
                    });
            }
        },

        cancel() {
            this.$router.back();
        }

    },
    created() {
        this.loadAllMeals();
        this.loadMeals();
    },
};
</script>
    