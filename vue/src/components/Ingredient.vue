<template>
  <section class="ingredient">
    <div>Quantity: {{ item.quantity }}</div>
    <div>Name: {{ item.ingredientName }}</div>
    <p></p>
  </section>
</template>

<script>
export default {
  name: "ingredient",
  props: ["item"],
};
</script>
