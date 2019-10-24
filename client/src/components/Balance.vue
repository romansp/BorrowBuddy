<template>
  <v-text-field 
    :value="amount"
    tabindex="-1" 
    label="Current balance"
    readonly
  />
</template>


<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    amount: {
      type: Number,
      default: 0
    }
  }
});
</script>
