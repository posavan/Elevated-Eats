<template>
  <section class="ingredient">
    <p>Name: {{ item.ingredientName }}</p>
    <p>Quantity: {{ item.ingredientQuantity }}</p>
    <p>Calories: {{ item.calories }}</p>
  </section>
</template>
  
<script>
export default {
  name: "ingredient",
  props: ["item"],
};
</script>
  
  