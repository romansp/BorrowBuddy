<template>
  <div>
    <CurrencyAdd />
  </div>
</template>

<script lang="ts">
import Vue from "vue";

import CurrencyAdd from "@/components/CurrencyAdd.vue";

export default Vue.extend({
  components: {
    CurrencyAdd
  }
});
</script>
