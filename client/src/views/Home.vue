<template>
  <div class="home">
    <FlowAdd />
  </div>
</template>

<script lang="ts">
import FlowAdd from "@/components/FlowAdd.vue";
import Vue from "vue";
export default Vue.extend({
  components: {
    FlowAdd
  }
});
</script>
