<template>
  <div id="capstone-app">
    <div id="nav">
      <span id="home">
       &nbsp;<router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      </span>
      <span id="mealplan">
      <router-link v-bind:to="{ name: 'mealplan' }" v-if="this.$store.state.token !=''">Meal Plans</router-link>&nbsp;
      </span>
      <span id="user">
      &nbsp;<router-link v-bind:to="{ name: 'favorites' }" v-if="this.$store.state.token != ''">My Recipes</router-link>&nbsp;
      </span>
      <span id="recipe">
      &nbsp;<router-link v-bind:to="{ name: 'recipe' }" v-if="this.$store.state.token != ''">Recipes</router-link>&nbsp;
      </span>
      <span id="meal">
      &nbsp;<router-link v-bind:to="{ name: 'meal' }" v-if="this.$store.state.token != ''">Meals </router-link>&nbsp;
      </span>
      <span id="logout">
       &nbsp;<router-link v-bind:to="{ name: 'logout' }" v-if="this.$store.state.token != ''">Logout</router-link>&nbsp;
      </span>
    </div>
    <router-view />
  </div>
</template>


<style>
html{
  background-color: white;
}
#nav {
 float: left;
  background-color: rgb(235, 142, 42);
  color: white;
  padding: 10px;
  background-blend-mode: multiply;
}


#home,
#recipe,
#user,
#mealplan,
#logout {
  font-size: 18px;
  margin-right: 10px;
}

#home a,
#recipe a,
#user a,
#meal a,
#mealplan a,
#logout a {
  text-decoration: none;
  color: white;
}
#home a:hover,
#recipe a:hover,
#meal a:hover,
#user a:hover,
#mealplan a:hover,
#logout a:hover {
  color: black;
}

#home a:hover,
#recipe a:hover,
#user a:hover,
#meal a:hover,
#mealplan a:hover,
#logout a:hover {
  text-decoration: underline;
  cursor: pointer;
}

</style>