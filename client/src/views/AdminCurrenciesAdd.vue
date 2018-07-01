<template>
  <v-dialog 
    v-model="opened"
    fullscreen
    hide-overlay 
    transition="dialog-bottom-transition">
    <v-card>
      <v-toolbar 
        dark 
        color="primary">
        <v-btn 
          :to="routeList" 
          exact
          icon 
          dark>
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>Add Currency</v-toolbar-title>
      </v-toolbar>
      <v-card-text>
        <CurrencyForm 
          v-model="currency" 
          @saved="goToList" />
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from "vue";

import CurrencyForm from "@/components/CurrencyForm.vue";
import { routes } from "@/router";

export default Vue.extend({
  components: {
    CurrencyForm
  },

  data() {
    return {
      opened: false,
      routeList: routes["admin.currency"],
      currency: {
        code: "",
        symbol: "",
        scale: 100
      }
    };
  },

  mounted() {
    this.opened = true;
  },

  methods: {
    goToList() {
      this.$router.push(this.routeList);
    }
  }
});
</script>
