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
          :to="{ name: 'admin.participant' }" 
          exact
          icon 
          dark>
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>Add User</v-toolbar-title>
      </v-toolbar>
      <v-card-text>
        <ParticipantForm 
          v-model="participant" 
          @saved="goToList" />
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from "vue";

import ParticipantForm from "@/components/ParticipantForm.vue";
import { routes } from "@/router";

export default Vue.extend({
  components: {
    ParticipantForm
  },

  data() {
    return {
      opened: false,
      routeList: routes["admin.participant"],
      participant: {
        firstName: "",
        lastName: "",
        middleName: ""
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
