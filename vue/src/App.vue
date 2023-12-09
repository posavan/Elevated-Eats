<template>
  <div id="capstone-app">
    <div id="nav">
      <span id="home">
        &nbsp;|&nbsp;<router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      </span>
      <span id="recipe">
      &nbsp;|&nbsp;<router-link v-bind:to="{ name: 'recipe' }" v-if="this.$store.state.token != ''">Recipes</router-link>&nbsp;|&nbsp;
      </span>
      <span id="user">
      &nbsp;|&nbsp;<router-link v-bind:to="{ name: 'favorites' }" v-if="this.$store.state.token != ''">MyRecipes</router-link>&nbsp;|&nbsp;
      </span>
      <!-- <span id="mealplan">
      <router-link v-bind:to="{ name: 'mealplans' }" v-if="$store.state.token != ''">Mealplans</router-link>&nbsp;|&nbsp;
      </span> -->
      <span id="logout">
        &nbsp;|&nbsp;<router-link v-bind:to="{ name: 'logout' }" v-if="this.$store.state.token != ''">Logout</router-link>&nbsp;|&nbsp;
      </span>
    </div>
    <router-view />
  </div>
</template>


<style>
html{
  background-color: wheat;
}
#nav {
 float: left;
  background-color: rgb(213, 51, 51);
  color: wheat;
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
#mealplan a,
#logout a {
  text-decoration: none;
  color: wheat;
}

#home a:hover,
#recipe a:hover,
#user a:hover,
#mealplan a:hover,
#logout a:hover {
  text-decoration: underline;
  cursor: pointer;
}

</style>