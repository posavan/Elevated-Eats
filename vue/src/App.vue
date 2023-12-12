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

h1 {
  font-family:Georgia, 'Times New Roman', Times, serif;
  text-align: center;
  font-weight: normal;
}
h2 {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
}
h3{
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
  
}

section.container {
  text-align: center;
}
p {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
}
div.button-container {
  text-align: center;
}
.container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
  gap: 1.25rem;
}
button {
  display: inline-block;
  border-radius: 1.5rem;
  cursor: pointer;
  padding: 0.5rem 1.5rem;
  text-decoration: none;
  white-space: wrap;
  text-transform: none;
  font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  font-feature-settings: normal;
  font-style: normal;
  letter-spacing: normal;
  line-break: auto;
  line-height: 1.25em;
  font-size: 16px;
  font-weight: 500;
  overflow-wrap: normal;
  border-width: .2rem;
  margin-bottom: 1.9rem;
  text-align: center;
}
button:hover {
  border-style: dotted;
}

section.recipe{
  text-align: center;
}
section.meal{
  background-color: rgb(255, 207, 111);
  
}

html{
  background-color: white;
}
#nav {
 /* float: left; */
 font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
 background-color: rgb(235, 142, 42);
  color: white;
  padding: 10px;
  background-blend-mode: multiply;
}


#home,
#recipe,
#user,
#mealplan,
#meal,
#logout {
  font-size: 18px;
  margin-right: 65px;
  text-align: center;
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