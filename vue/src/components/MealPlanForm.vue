<template>
  <form v-on:submit.prevent="createNewMealPlan">
    <div>
      <label for="name">Name: </label>
      <input type="text" name="name" id="name" v-model="editMealPlan.mealPlanName" />
    </div>
    <div>
      <label for="description">Description: </label>
      <input type="text" name="description" id="description" v-model="editMealPlan.mealPlanDescription" />
    </div>
    <!-- display meals to be added -->
    <meal v-for="meal in addedMeals" v-bind:key="meal.id" v-bind:item="meal" />
    <!-- <button class="btn-remove" type="button" @click="removeMeal">Remove Meal</button> -->
    <div>
      <!-- add each meal -->
      <label for="meals">Meals:</label>
      <select v-model="selected">
        <option v-for="meal in meals" v-bind:key="meal.id" :value="meal">
          {{ meal.mealName }}
        </option>
      </select>
      <button class="btn-add-meal" type="button" @click="addMeal">Add Meal</button>
    </div>

    <button class="btn-create-meal" type="button" @click="this.$router.push({ name: 'createMeal' })"
      v-if="showCreate">Create
      New Meal</button>
    <div class="actions">
      <button class="btn-submit" type="submit">Save Meal Plan</button>
      <button class="btn-cancel" type="button" @click="cancel" v-if="show">Cancel</button>
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
      addedMeals: [],
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
      mealService
        .list()
        .then((response) => {
          console.log("reached loadMeals", response.data);
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
      this.addedMeals.push(this.selected);
    },

    createNewMealPlan() {
      this.editMealPlan.mealList = this.addedMeals;
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
    this.loadMeals();
  },
};
</script>
  