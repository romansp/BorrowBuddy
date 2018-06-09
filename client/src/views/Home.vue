<template>
  <div class="home">
    <img src="../assets/logo.png">
    <BAddFlow />
  </div>
</template>

<script lang="ts">
import BAddFlow from "@/components/BAddFlow.vue";
import Vue from "vue";
export default Vue.extend({
  components: {
    BAddFlow
  }
});
</script>
