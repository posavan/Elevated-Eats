<template>
  <section class="ingredient">
    <div v-if="showQuantity">Quantity: {{ item.quantity }}</div>
    <div>Name: {{ item.ingredientName }}</div>
    <p></p>
  </section>
</template>

<script>
export default {
  name: "ingredient",
  props: ["item"],
  data() {
    return {
      showQuantity: this.$route.name == 'userRecipe'
    }
  }
};
</script>
