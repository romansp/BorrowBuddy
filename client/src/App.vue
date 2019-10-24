<template>
  <v-app>
    <BbNavigationDrawer v-model="drawer" />
    <v-app-bar app>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title>Borrow Buddy</v-toolbar-title>
    </v-app-bar>
    <v-content>
      <v-container fluid>
        <router-view />
      </v-container>
    </v-content>
    <v-footer app />
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";

import BbNavigationDrawer from "@/components/BbNavigationDrawer.vue";

export default Vue.extend({
  components: {
    BbNavigationDrawer
  },

  data: () => ({
    drawer: false
  })
});
</script>

<style lang="scss">
@import "styles/app";
</style>
