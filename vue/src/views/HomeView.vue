<template>
  <div class="home">
    <h1>Home</h1>
    <!-- <p>You must be authenticated to see this</p> -->
    <nav class="recipes">
      <a href="recipe">Recipes </a>
    </nav>
    <nav class="my-recipes">
      <a href="recipe/favorites">My Recipes</a>
    </nav>
    <!-- <nav class="meal-plans">
      <a href="mealplans">My Meal Plans</a>
    </nav> -->
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
  padding-top: 100px;
  padding-bottom: 13%;
  padding-top: 240px;
  background-size: cover;
  background-blend-mode: multiply;
}

h1 {
  text-align: center;
  font-size: 100px;
}

p {
  text-align: center;
}

.recipes,
.meal-plans,
.my-recipes {
  display: block;
  color: wheat;
  text-align: center;
  padding: 14px 16px;
  background-color: rgb(213, 51, 51);
  border-radius: 50px;
  margin-left: 40%;
  margin-right: 40%;
  border-top: none;
  border-left: none;
  border-right: none;
  margin-bottom: 0.9%;
  text-decoration: none;
  font-family: sans-serif;
  font-size: 16px;
}

.recipes:hover {
  opacity: .50;
}

.my-recipes:hover {
  opacity: 0.50;
}

.meal-plans:hover {
  opacity: 0.50;
}</style>
