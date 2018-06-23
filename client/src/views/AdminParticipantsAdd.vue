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
          :to="{ name: 'admin.participants' }" 
          exact
          icon 
          dark>
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>Add User</v-toolbar-title>
        <v-spacer/>
        <v-toolbar-items>
          <v-btn 
            :to="{ name: 'admin.participants' }" 
            exact
            dark 
            flat>Save</v-btn>
        </v-toolbar-items>
      </v-toolbar>
      <v-list 
        three-line 
        subheader>
        <v-subheader>Add User</v-subheader>
        <ParticipantAdd />
      </v-list>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from "vue";

import ParticipantAdd from "@/components/ParticipantAdd.vue";

export default Vue.extend({
  components: {
    ParticipantAdd
  },

  data() {
    return {
      opened: false
    };
  },

  mounted() {
    this.opened = true;
  }
});
</script>
