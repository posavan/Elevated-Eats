<template>
  <div id="login">
    <form v-on:submit.prevent="login" style="background-color: rgb(246, 219, 191);">
      <h1>Sign In</h1>
      <div role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>

      <div role="alert" v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div class="form-input-group">
        <label for="username">Username</label>
        <input
          type="text"
          id="username"
          v-model="user.username"
          required
          autofocus
        />
      </div>
      <div class="form-input-group">
        <label for="password">Password</label>
        <input type="password" id="password" v-model="user.password" required />
      </div>
      <button type="submit">Sign in</button>
      <p>
        <router-link v-bind:to="{ name: 'register' }" style="color:rgb(168, 89, 15) ;"
          >Need an account? Sign up.</router-link
        >
      </p>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>


<style scoped>
h1{
  text-align: center;
  color:rgb(235, 142, 42);
  margin-left: .1%;
  font-weight: bold;
  
}

.form-input-group {
  margin-bottom: 1rem;
  text-align: center;
}

label {
  color: black;
  margin-right: 70px;
}

div#login{
  font-family: Georgia, 'Times New Roman', Times, serif;
  background-color: rgb(254, 251, 251);
  background-image: url("/img/login.avif");
  color: white;
  padding-top: 100px;
  padding-bottom: 13%;
  padding-top: 240px;
  background-size: cover;
  background-blend-mode: darken;

}

form {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  font-size: large;
  text-align: center;
  background-color: rgb(204, 204, 204);
  padding-top: 10px;
  padding-bottom: 0px;
  

  
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

p {
  margin: 20px;
  padding-bottom: 7%;
  padding-top: 1%;
}

input#username {
  width: 12%;
  height:35px;
  margin-bottom: 15px;
  background: white;
  border-bottom: 1px solid #fff;
  border-top: none;
  border-left: none;
  border-right: none;
  text-align: center;
}

input#password {
  width: 12%;
  height: 25px;
  padding-top: 10px;
  margin-bottom: 15px;
  text-align: center;
  border-radius: 1rem;
  background: white;
  border-bottom: 1px solid #fff;
  border-top: none;
  border-left: none;
  border-right: none;
 
}
</style>
