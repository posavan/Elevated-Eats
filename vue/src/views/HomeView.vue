<template>
  <div class="home">
    <h1>Elevated Eats</h1>
    <!-- <p>You must be authenticated to see this</p> -->
    <nav class="meal-plans">
      <a href="/mealplan" class="fill-nav" style="
          text-decoration: none;
          color: white;
          font-size: 20px;
        ">My Meal Plans</a>
    </nav>
    <nav class="my-recipes">
      <a href="/recipe/favorites" class="fill-nav" style="
          text-decoration: none;
          color: white;
          font-size: 20px;
        ">Favorite Recipes</a>
    </nav>
    <nav class="recipes">
      <a href="/recipe" class="fill-nav" style="
          text-decoration: none;
          color: white;
          font-size: 20px;
        ">Recipes</a>
    </nav>
    <nav class="meals">
      <a href="/meal" class="fill-nav" style="
          text-decoration: none;
          color: white;
          font-size: 20px;
        ">Meals</a>
    </nav>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
div.home {
  font-family: Georgia, 'Times New Roman', Times, serif;
  background-color: white;
  background-image: url("/img/homeview.jpg");
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

section.container {
  display: block;
}

p {
  text-align: center;
}

a {
  color: white;
  text-decoration: none;
}

a:hover {
  color: black;
  text-decoration: underline;
}

.recipes,
.meal-plans,
.meals,
.my-recipes {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  display: block;
  text-align: center;
  padding: 13px 16px;
  background-color: rgb(235, 142, 42);
  border-radius: 50px;
  margin-left: 40%;
  margin-right: 40%;
  border-top: none;
  border-left: none;
  border-right: none;
  margin-bottom: 0.9%;
  text-decoration: none;
  font-size: 16px;
  color: inherit;
}

a.fill-nav {
  display: block;
  height: 100%;
  width: 100%;
  text-decoration: none;
}

.recipes:hover {
  border-color: white;
  border-style: solid;
  border-width: .325rem;
}

.my-recipes:hover {
  border-color: white;
  border-style: solid;
  border-width: .325rem;
}

.meal-plans:hover {
  border-color: white;
  border-style: solid;
  border-width: .325rem;
}

.meals:hover {
  border-color: white;
  border-style: solid;
  border-width: .325rem;
}
</style>
