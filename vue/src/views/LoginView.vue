<template>
  <div id="login">
    <form v-on:submit.prevent="login">
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
        <router-link v-bind:to="{ name: 'register' }"
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
div#login {
  background-color:rgb(186, 48, 48);
  border-radius: 10%;
  
}
.form-input-group {
  margin-bottom: 1rem;
}
label {
  margin-right: 0.5rem;
}
form {
  padding-top: 15%;
  background-color: wheat;
  font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
  font-size: large;
  text-align: center;
  border-radius: 50%;
  padding: auto;
  color:rgb(95, 25, 133);
}
button {
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  background-color: brown;
  border-radius: 50px;
  margin-left: 48%;
  border-top: none;
  border-left: none;
  border-right: none;
  border-bottom: none;
}
p {
  margin: 20px;
  padding-bottom: 15%;
  padding-top: 1%;
}
input#username {
  width: 10%;
  height: 25px;
  padding-top: 10px;
  margin-bottom: 15px;
  background: rgb(252, 235, 234);
  border-bottom: 1px solid #fff;
  border-top: none;
  border-left: none;
  border-right: none;
  text-align: center;
  font-size: large;
}
input#password {
  width: 10%;
  height: 25px;
  padding-top: 10px;
  margin-bottom: 15px;
  background: rgb(252, 235, 234);
  border-bottom: 1px solid #fff;
  border-top: none;
  border-left: none;
  border-right: none;
  text-align: center;
  font-size: large;
}
</style>
