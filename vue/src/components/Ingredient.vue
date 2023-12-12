<template>
  <section class="ingredient">
    <div>Name: {{ item.ingredientName }}</div>
    <div v-if="!hideQuantity">Quantity: {{ item.quantity }}</div>
    <p></p>
  </section>
</template>

<script>
export default {
  name: "ingredient",
  props: ["item"],
  data() {
    return {
      hideQuantity: this.$route.name == 'ingredients'
    }
  }
};
</script>
