<template>
  <v-navigation-drawer 
    v-model="drawer" 
    absolute
    temporary
    hide-overlay
    app>
    <v-list>
      <v-list-tile 
        :to="{ name: 'home'}" 
        exact>
        <v-list-tile-action>
          <v-icon>home</v-icon>
        </v-list-tile-action>
        <v-list-tile-title>Home</v-list-tile-title>
      </v-list-tile>
      <v-list-tile :to="{ name: 'history'}" >
        <v-list-tile-action>
          <v-icon>history</v-icon>
        </v-list-tile-action>
        <v-list-tile-title>History</v-list-tile-title>
      </v-list-tile>
      <v-list-group 
        group="admin"
        prepend-icon="settings" 
        no-action>
        <v-list-tile slot="activator">
          <v-list-tile-title>Settings</v-list-tile-title>
        </v-list-tile>
        <v-list-tile :to="{ name: 'admin.participant' }">
          <v-list-tile-content>
            <v-list-tile-title>Users</v-list-tile-title>
          </v-list-tile-content>
          <v-list-tile-action>
            <v-icon>account_circle</v-icon>
          </v-list-tile-action>
        </v-list-tile>
        <v-list-tile :to="{ name: 'admin.currency' }">
          <v-list-tile-content>
            <v-list-tile-title>Currencies</v-list-tile-title>
          </v-list-tile-content>
          <v-list-tile-action>
            <v-icon>attach_money</v-icon>
          </v-list-tile-action>
        </v-list-tile>
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
