<template>
  <v-navigation-drawer 
    v-model="drawer" 
    absolute
    temporary
    hide-overlay
    app
  >
    <v-list>
      <v-list-item 
        :to="{ name: 'home'}" 
        exact
      >
        <v-list-item-action>
          <v-icon>home</v-icon>
        </v-list-item-action>
        <v-list-item-title>Home</v-list-item-title>
      </v-list-item>
      <v-list-item :to="{ name: 'history'}">
        <v-list-item-action>
          <v-icon>history</v-icon>
        </v-list-item-action>
        <v-list-item-title>History</v-list-item-title>
      </v-list-item>
      <v-list-group 
        group="admin"
        prepend-icon="settings" 
        no-action
      >
        <v-list-item slot="activator">
          <v-list-item-title>Settings</v-list-item-title>
        </v-list-item>
        <v-list-item :to="{ name: 'admin.participant' }">
          <v-list-item-content>
            <v-list-item-title>Users</v-list-item-title>
          </v-list-item-content>
          <v-list-item-action>
            <v-icon>account_circle</v-icon>
          </v-list-item-action>
        </v-list-item>
        <v-list-item :to="{ name: 'admin.currency' }">
          <v-list-item-content>
            <v-list-item-title>Currencies</v-list-item-title>
          </v-list-item-content>
          <v-list-item-action>
            <v-icon>attach_money</v-icon>
          </v-list-item-action>
        </v-list-item>
      </v-list-group>
    </v-list>
  </v-navigation-drawer>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    value: {
      type: Boolean,
      default: false
    }
  },

  computed: {
    drawer: {
      get(): boolean {
        return this.value;
      },
      set(open: boolean) {
        this.$emit("input", open);
      }
    }
  }
});
</script>
