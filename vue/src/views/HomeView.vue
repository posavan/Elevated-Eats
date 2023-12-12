<template>
  <div class="home">
    <h1>Elevated Eats</h1>
    <!-- <p>You must be authenticated to see this</p> -->
    <nav class="meal-plans">
      <a
        href="/mealplan"
        style="
          text-decoration: none;
          color: white;
          font-size: 20px;
        "
        >My Meal Plans</a
      >
    </nav>
    <nav class="my-recipes">
      <a
        href="/recipe/favorites"
        style="
          text-decoration: none;
          color: white;
          font-size: 20px;
        "
        >My Saved Recipes</a
      >
    </nav>
    <nav class="recipes">
      <a
        href="/recipe"
        style="
          text-decoration: none;
          color: white;
          font-size: 20px;
        
        "
        >All Recipes</a
      >
    </nav>
    <nav class="meals">
      <a
        href="/meal"
        style="
          text-decoration: none;
          color: white;
          font-size: 20px;
        "
        >Meals</a
      >
    </nav>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
div.home {
  font-family:Georgia, 'Times New Roman', Times, serif;
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
section.container{
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

.recipes:hover {
  border-color: rgb(8, 155, 21);
  border-style: solid;
  border-width: .325rem;
}

.my-recipes:hover {
  border-color: rgb(8, 155, 21);
  border-style: solid;
  border-width: .325rem;
}

.meal-plans:hover {
  border-color: rgb(8, 155, 21);
  border-style: solid;
  border-width: .325rem;
}
.meals:hover {
  border-color: rgb(8, 155, 21);
  border-style: solid;
  border-width: .325rem;
}
</style>
