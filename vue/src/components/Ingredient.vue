<template>
    <section class="ingredient">
       <p>Id: {{ item.ingredientId }}</p>
      <p>Name: {{ item.ingredientName }}</p>
      <p>Calories: {{ item.calories }}</p>
    </section>
  </template>
  
  <script>
  export default {
    name: "ingredient",
    props: ["item"],
  };
  </script>
  
  