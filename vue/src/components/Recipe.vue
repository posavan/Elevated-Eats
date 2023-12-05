<template>
  <section class="recipe">
    <p>Name: {{ item.recipeName }}</p>
    <p>Description: {{ item.recipeDescription }}</p>
    <p>Ingredients: </p>
    <section class="container">
      <ingredient v-for="ingredient in item.ingredientList" v-bind:key="ingredient.id" v-bind:item="ingredient">
      {{ item.ingredientList }}</ingredient>
      </section>
  </section>
</template>
  
<script>
export default {
  name: "recipe",
  props: ["item"],
};
</script>
  
  