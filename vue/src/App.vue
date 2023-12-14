<template>
  <div id="capstone-app">
    <div id="nav">
      <li><span id="home" >
        &nbsp;<router-link v-bind:to="{ name: 'home' }"><img src="/img/homeIcon.png" style="padding: 0; margin: 0; border:0"> </router-link>&nbsp; &nbsp;
      </span></li>
      <li><span id="mealplan">
        <router-link v-bind:to="{ name: 'mealplan' }" v-if="this.$store.state.token != ''" style="
          text-decoration: none; color: white;">My Meal
          Plans</router-link>&nbsp;
      </span></li>
      <li><span id="user">
        &nbsp;<router-link v-bind:to="{ name: 'favorites' }" v-if="this.$store.state.token != ''" style="
          text-decoration: none; color: white;">My
          Recipes</router-link>&nbsp;
      </span></li>
      <li><span id="recipe">
        &nbsp;<router-link v-bind:to="{ name: 'recipe' }" v-if="this.$store.state.token != ''" style="
          text-decoration: none; color: white;">Recipes</router-link>&nbsp;
      </span></li>
      <li><span id="meal">
        &nbsp;<router-link v-bind:to="{ name: 'meal' }" v-if="this.$store.state.token != ''" style="
          text-decoration: none; color: white;">Meals </router-link>&nbsp;
      </span></li>
      <li><span id="logout">
        &nbsp;&nbsp;<router-link v-bind:to="{ name: 'logout' }"
          v-if="this.$store.state.token != ''" style="
          text-decoration: none; color: white;">Logout</router-link>&nbsp;
      </span></li>
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


#nav {
  list-style-type: none;
  margin-bottom: 0;
  padding-bottom: 0;
  overflow: hidden;
  background-color: rgb(235, 142, 42);
  position: fixed; 
  top: 0;
  width: 100%;
  height: 50px;
  font-size: 18px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  z-index: 99999;
}

li{ 
  float: left;
  border-right: 1px solid white;

}
li span#home{
  padding-top: 8px;
  padding-right: 3px;
  
  
}

li span{
  display: block;
  color: #fff;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

li span:hover{
  background-color:  rgb(37, 36, 42);
}

.active {
  background-color: black;
}

h1 {
  margin-top: 6%;
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


html {
  background-color: white;
}
#recipe,
#user,
#mealplan,
#meal
{
  font-size: 18px;
  text-align: center;
}

/* #nav {
  
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background-color: rgb(245, 141, 30);
  color: white;
  padding: 20px;
  background-blend-mode: multiply;
  
}


#logout {
  font-weight: bold;
  font-size: 18px;
  margin-left: 250px;
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
  text-align: center;
}

#home a:hover,
#recipe a:hover,
#meal a:hover,
#user a:hover,
#mealplan a:hover,
#logout a:hover {
  color: black;
  text-decoration: underline;
  cursor: pointer;
} */


</style>
