<template>
  <div class="home">
    <h1>Home</h1>
    <!-- <p>You must be authenticated to see this</p> -->
    <nav class="recipes">
      <a href="/recipe">Recipes </a>
    </nav>
    <nav class="my-recipes">
      <a href="/recipe/1">My Recipes</a>
    </nav>
    <nav class="meal-plans">
      <a href="/mealPlans">My Meal Plans</a>
    </nav>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
div.home {
  background-color: rgb(120, 111, 84);
  background-image: url("../../homeview.jpg");
  color: wheat;
  padding-top: 200px;
  padding-bottom: 13%;
  padding-top: 240px;
  background-size: cover;
  border-radius: 3%;
  background-blend-mode: multiply;
}
h1 {
  text-align: center;
  font-size: 100px;
}
p {
  text-align: center;
}
.recipes {
  display: block;
  color: wheat;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  background-color: rgb(213, 51, 51);
  border-radius: 50px;
  margin-left: 40%;
  margin-right: 40%;
  margin-bottom: 0.9%;
  border-top: none;
  border-left: none;
  border-right: none;
}
.meal-plans {
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  background-color: rgb(213, 51, 51);
  border-radius: 50px;
  margin-left: 40%;
  margin-right: 40%;
  margin-bottom: 0.9%;
  border-top: none;
  border-left: none;
  border-right: none;
  border-bottom: none;
}
.my-recipes {
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
  background-color: rgb(213, 51, 51);
  border-radius: 50px;
  margin-left: 40%;
  margin-right: 40%;
  border-top: none;
  border-left: none;
  border-right: none;
  border-bottom: none;
  margin-bottom: 0.9%;
}
</style>
