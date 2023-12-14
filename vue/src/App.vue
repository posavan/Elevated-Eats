<template>
  <div id="capstone-app">
    <div id="nav">
      <span id="home">
        &nbsp;<router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      </span>
      <span id="mealplan">
        <router-link v-bind:to="{ name: 'mealplan' }" v-if="this.$store.state.token != ''">My Meal
          Plans</router-link>&nbsp;
      </span>
      <span id="user">
        &nbsp;<router-link v-bind:to="{ name: 'favorites' }" v-if="this.$store.state.token != ''">My
          Recipes</router-link>&nbsp;
      </span>
      <span id="recipe">
        &nbsp;<router-link v-bind:to="{ name: 'recipe' }" v-if="this.$store.state.token != ''">Recipes</router-link>&nbsp;
      </span>
      <span id="meal">
        &nbsp;<router-link v-bind:to="{ name: 'meal' }" v-if="this.$store.state.token != ''">Meals </router-link>&nbsp;
      </span>
      <span id="logout">
        &nbsp;|&nbsp;<router-link v-bind:to="{ name: 'logout' }"
          v-if="this.$store.state.token != ''">Logout</router-link>&nbsp;
      </span>
    </div>
    <router-view />
  </div>
</template>

<style>
body {
  margin: 0%;
  padding: 0%;
  border: 0%;
}

h1 {
  font-family: Georgia, 'Times New Roman', Times, serif;
  text-align: center;
  font-weight: normal;
}

h2 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
}

h3 {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
}

section.container {
  text-align: center;
  position: center;
}

p {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  text-align: center;
}

form {
  margin-left: 100px;
  margin-right: 100px;
}

textarea {
  width: 60%;
  height: 200px;
}
div.button-container {
  text-align: center;
  padding: 2%;
}

.container {
  display: grid;
  border-width: 1rem;
  grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
  gap: 1.25rem;
  position: center;
  padding-bottom: 0%;
}

.button{
  align-self: center;
  background-color: #fff;
  background-image: none;
  background-position: 0 90%;
  background-repeat: repeat no-repeat;
  background-size: 4px 3px;
  border-radius: 15px 225px 255px 15px 15px 255px 225px 15px;
  border-style: solid;
  border-width: 2px;
  box-shadow: rgba(0, 0, 0, .2) 15px 28px 25px -18px;
  box-sizing: border-box;
  color: #41403e;
  cursor: pointer;
  display: inline-block;
  font-family: Neucha, sans-serif;
  font-size: 1rem;
  line-height: 23px;
  outline: none;
  padding: .75rem;
  text-decoration: none;
  transition: all 235ms ease-in-out;
  border-bottom-left-radius: 15px 255px;
  border-bottom-right-radius: 225px 15px;
  border-top-left-radius: 255px 15px;
  border-top-right-radius: 15px 225px;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
}

.button:hover {
  box-shadow: rgba(0, 0, 0, .3) 2px 8px 8px -5px;
  transform: translate3d(0, 2px, 0);
}

.button:focus {
  box-shadow: rgba(0, 0, 0, .3) 2px 8px 4px -6px;
}


/* button:hover {
  border-style: dotted;
} */

/* section.recipe {
  text-align: center;
}
section.meal {
  background-color: rgb(255, 207, 111);
} */

html {
  background-color: white;
}

#nav {
  /* float: left; */
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background-color: rgb(245, 141, 30);
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
