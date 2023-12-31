import { createRouter as createRouter, createWebHistory } from "vue-router";
import { useStore } from "vuex";

// Import components
import HomeView from "../views/HomeView.vue";
import LoginView from "../views/LoginView.vue";
import LogoutView from "../views/LogoutView.vue";
import RegisterView from "../views/RegisterView.vue";

import ListIngredientsView from "../views/ListIngredientsView.vue";
import ListRecipesView from "../views/ListRecipesView.vue";
import ListAllRecipesView from "../views/ListAllRecipesView.vue";

import AddIngredientView from "../views/AddIngredientView.vue";

import RecipeDetailsView from "../views/RecipeDetailsView.vue";
import AddRecipeView from "../views/AddRecipeView.vue";
import UserRecipeDetailsView from "../views/UserRecipeDetailsView.vue";
import EditRecipeView from "../views/EditRecipeView.vue";

import MealView from "../views/MealView.vue";
import MealDetailsView from "../views/MealDetailsView.vue";
import AddMealView from "../views/AddMealView.vue";
import EditMealView from "../views/EditMealView.vue";

import MealPlanView from "../views/MealPlanView.vue";
import AddMealPlanView from "../views/AddMealPlanView.vue";
import MealPlanDetailsView from "../views/MealPlanDetailsView.vue";
import EditMealPlanView from "../views/EditMealPlanView.vue";
import GroceriesView from "../views/GroceriesView.vue";
import ListRecipeIngredientsView from "../views/ListRecipeIngredientsView.vue";

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */
const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/login",
    name: "login",
    component: LoginView,
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/logout",
    name: "logout",
    component: LogoutView,
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/register",
    name: "register",
    component: RegisterView,
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/ingredients",
    name: "ingredients",
    component: ListIngredientsView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/recipe",
    name: "recipe",
    component: ListAllRecipesView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/recipe/favorites",
    name: "favorites",
    component: ListRecipesView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/recipe/create",
    name: "createRecipe",
    component: AddRecipeView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/recipe/public/:recipeName",
    name: "recipeDetails",
    component: RecipeDetailsView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/recipe/favorites/:recipeId",
    name: "userRecipeDetails",
    component: UserRecipeDetailsView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/ingredients/create",
    name: "addIngredient",
    component: AddIngredientView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/meal",
    name: "meal",
    component: MealView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/meal/:mealId",
    name: "mealDetailsView",
    component: MealDetailsView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/meal/:mealId/edit",
    name: "EditMealView",
    component: EditMealView,
    meta: {
      requiresAuth: true,
    },
  },
  
  // {
  //   path: "/recipe/favorites/:recipeId/ingredients",
  //   name: "listRecipeIngredients",
  //   component: ListRecipeIngredientsView,
  //   meta: {
  //     requiresAuth: true,
  //   },
  // },
  {
    path: "/recipe/favorites/:recipeId/edit",
    name: "editRecipe",
    component: EditRecipeView,
    meta: {
      requiresAuth: true,
    },
  },
  // {
  //   path: "/recipe/:userId/:recipeId",
  //   name: "addRecipeToUser",
  //   component: AddRecipeView,
  //   meta: {
  //     requiresAuth: true,
  //   },
  // },
  {
    path: "/meal/create",
    name: "createMeal",
    component: AddMealView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/mealplan",
    name: "mealplan",
    component: MealPlanView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/mealplan/add",
    name: "addMealPlan",
    component: AddMealPlanView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/mealplan/:mealPlanId",
    name: "mealPlanDetails",
    component: MealPlanDetailsView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/mealplan/:mealPlanId/edit",
    name: "editMealPlan",
    component: EditMealPlanView,
    meta: {
      requiresAuth: true,
    },
  },
  {
    path: "/mealplan/:mealPlanId/groceries",
    name: "groceries",
    component: GroceriesView,
    meta: {
      requiresAuth: true,
    },
  },
];

// Create the router
const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});

router.beforeEach((to) => {
  // Get the Vuex store
  const store = useStore();
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some((x) => x.meta.requiresAuth);
  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === "") {
    return { name: "login" };
  }
  // Otherwise, do nothing and they'll go to their next destination
});

export default router;
