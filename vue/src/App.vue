<template>
  <div id="capstone-app">
    <div id="nav">
      <span id="home">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      </span>
      <span id="logout">
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
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
#logout {
  font-size: 18px;
  margin-right: 10px;
}

#home a,
#logout a {
  text-decoration: none;
  color: wheat;
}

#home a:hover,
#logout a:hover {
  text-decoration: underline;
  cursor: pointer;
}

</style>