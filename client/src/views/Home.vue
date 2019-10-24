<template>
  <div class="home">
    <router-view />
  </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  async mounted() {
    this.$store.dispatch("PARTICIPANTS_FETCH");
    this.$store.dispatch("CURRENCIES_FETCH");
  }
});
</script>
