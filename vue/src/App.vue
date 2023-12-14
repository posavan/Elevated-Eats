<template>
  <div id="capstone-app" >
    <!-- <img id="background" src="/img/wood3.webp"> -->
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
          text-decoration: none; color: white;">Favorite Recipes</router-link>&nbsp;
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
  font-family: Georgia, 'Times New Roman', Times, serif;
  font-weight: normal;
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
  background-color:  rgb(70, 69, 70);
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


.button {
  font-size: 16px;
  font-weight: 200;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  letter-spacing: 1px;
  padding: 13px 20px 13px;
  outline: 0;
  border: 1px solid black;
  cursor: pointer;
  position: relative;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
}

button:after {
  content: "";
  background-color:rgb(254, 178, 108);
  width: 100%;
  z-index: -1;
  position: absolute;
  height: 100%;
  top: 7px;
  left: 7px;
  transition: 0.2s;
}

.button:hover {
  box-shadow: rgba(0, 0, 0, .3) 2px 8px 8px -5px;
  transform: translate3d(0, 2px, 0);
}

.button:focus {
  box-shadow: rgba(0, 0, 0, .3) 2px 8px 4px -6px;
}


html {
  background-color: rgb(255, 227, 190);
  
}
#recipe,
#user,
#mealplan,
#meal
{
  font-size: 18px;
  text-align: center;
}



</style>
