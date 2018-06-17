<template>
  <span>{{ balance }}</span>
</template>


<script lang="ts">
import { balance } from "@/services/participants.service";
import Vue from "vue";
export default Vue.extend({
  props: {
    from: {
      type: String,
      default: ""
    },

    to: {
      type: String,
      default: ""
    }
  },

  data() {
    return {
      balance: 0
    };
  },

  watch: {
    from() {
      this.updateBalance();
    },
    to() {
      this.updateBalance();
    }
  },

  methods: {
    async updateBalance() {
      if (this.from && this.to) {
        this.balance = await balance(this.from, this.to);
      }
    }
  }
});
</script>
