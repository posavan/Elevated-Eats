<template>
    <section class="recipe">
       <p>Id: {{ item.recipeId }}</p>
      <p>Name: {{ item.recipeName }}</p>
      <p>Description: {{ item.recipeDescription }}</p>
    </section>
  </template>
  
  <script>
  export default {
    name: "recipe",
    props: ["item"],
  };
  </script>
  
  