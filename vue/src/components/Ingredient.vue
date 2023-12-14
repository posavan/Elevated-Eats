<template>
  <section class="ingredient">
    <div class="column">
      <input type="checkbox" id="checkbox" v-model="checked" v-if="inGroceries" />
      <span v-if="!hideQuantity">{{ item.quantity }}</span>&nbsp;
      <span>{{ item.ingredientName }}</span>
    </div>
  </section>
</template>

<script>
export default {
  name: "ingredient",
  props: ["item"],
  data() {
    return {
      hideQuantity: this.$route.name == 'ingredients',
      inGroceries: this.$route.name == "groceries",
    }
  },

};
</script>

<style>


section.ingredient {
  
  /* justify-content: center; */
  align-items: center;
}

.column {
  /* border: 3px rgb(216, 117, 81) solid; */
  text-align: justify;
  padding: 10px;
}
</style>