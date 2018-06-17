<template>
  <div>
    <form 
      method="post" 
      @submit.prevent="submit">
      <div>
        Code:
        <input 
          v-model="code" 
          type="text" 
          name="Code">
      </div>
      <div>
        Symbol:
        <input 
          v-model="symbol" 
          type="text" 
          name="Symbol">
      </div>
      <div>
        Scale:
        <input 
          v-model="scale" 
          type="number" 
          name="Scale">
      </div>
      <button type="submit">OK</button>
    </form>
  </div>
</template>


<script lang="ts">
import Vue from "vue";

import { add } from "@/services/currency.service";

export default Vue.extend({
  data() {
    return {
      code: "BYN",
      symbol: "",
      scale: 100
    };
  },

  methods: {
    async submit() {
      const { code, symbol, scale } = this;
      await add({
        code,
        symbol,
        scale
      });
    }
  }
});
</script>
