<template>
    <h1 v-if="isLoading == false">Edit Meal Plan: {{ editMealPlan.mealPlanName }}</h1>
    <edit-meal-plan-form v-bind:mealplan="editMealPlan" v-if="isLoading == false" />

    <!--needs add ingredient button that allows user to add an ingredient not already part of this recipe-->
    <!--add ingredient button brings up a list of ingredients from ListIngredientsView-->
</template>
  
<script>
import mealPlanService from "../services/MealPlanService";
import EditMealPlanForm from "../components/EditMealPlanForm.vue";

export default {
    components: { EditMealPlanForm },
    prop: {
        mealplan: {
            type: Object,
            required: true,
        },
    },
    data() {
        return {
            isLoading: true,
            editMealPlan: {
                mealPlanId: this.mealPlanId,
                mealPlanName: this.mealPlanName,
                mealPlanDescription: this.mealPlanDescription,
            },
        };
    },
    methods: {
        loadMealPlan() {
            mealPlanService
                .getMealPlan(this.editMealPlan.mealPlanId)
                .then((response) => {
                    console.log(response.data);
                    this.editMealPlan = response.data;
                    this.isLoading = false;
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error loading mealplan: ", error.response.status);
                    } else if (error.request) {
                        console.log("Error loading mealplan: unable to communicate to server");
                    } else {
                        console.log("Error loading mealplan: make request");
                    }
                });
        },

        updateMealPlan() {
            mealPlanService
                .updateMealPlan(this.editMealPlan)
                .then((response) => {
                    this.$router.push({
                        name: "EditMealPlanView",
                        params: { id: this.editMealPlan.mealPlanId },
                    });
                    console.log("Meal plan edited successfully", response);
                })
                .catch((error) => {
                    if (error.response) {
                        console.log("Error updating Meal: ", error.response.status);
                    } else if (error.request) {
                        console.log("Error updating Meal: unable to communicate to server");
                    } else {
                        console.log("Error updating Meal: make request");
                    }
                });
        },

    },

    created() {
        this.editMealPlan.mealPlanId = this.$route.params.mealPlanId;
        this.loadMealPlan();
    }
};
</script>
  
  <style scoped>
h1{
    text-align: center;
}
</style>