<template>
  <div class="home">
    <h1>Home</h1>
    <!-- <p>You must be authenticated to see this</p> -->
    <nav class="meal-plans">
      <a href="/mealplan" style="text-decoration: none; color: white; font-size: 20px; font-family: Times New Roman;">My Meal Plans</a>
    </nav>
    <nav class="my-recipes"> 
      <a href="/recipe/favorites" style="text-decoration: none; color: white; font-size: 20px; font-family: Times New Roman;">My Saved Recipes</a>
    </nav>
    <nav class="recipes">
      <a href="/recipe" style="text-decoration: none; color: white; font-size: 20px; font-family: Times New Roman;">All Recipes</a>
    </nav>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
div.home {
  background-color: white;
  background-image: url("../../homeview.jpg");
  color: white;
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

a {
  color: white;
  text-decoration: none;
}

a:hover {
  text-decoration: underline;
  cursor: pointer;
}

.recipes,
.meal-plans,
.my-recipes {
  display: block;
  /* color: wheat; */
  text-align: center;
  padding: 14px 16px;
  background-color:rgb(235, 142, 42);
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
  color: inherit;
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
