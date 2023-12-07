<template>
  <section class="ingredient">
    <p>Name: {{ item.ingredientName }}</p>
    <p>Quantity: {{ item.quantity }}</p>
  </section>
</template>

<script>
export default {
  name: "ingredient",
  props: ["item"],
};
</script>
