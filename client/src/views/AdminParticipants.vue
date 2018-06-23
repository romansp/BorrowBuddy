<template>
  <div>
    <v-card>
      <ParticipantsList 
        :items="participants"/>
      <v-card-text style="position: relative">
        <v-btn
          fixed
          dark
          fab
          bottom
          right
          color="pink"
          @click.stop="dialogAdd = true"
        >
          <v-icon>add</v-icon>
        </v-btn>
      </v-card-text>
    </v-card>
    <v-dialog 
      v-model="dialogAdd" 
      fullscreen 
      hide-overlay 
      transition="dialog-bottom-transition">
      <v-card>
        <v-toolbar 
          dark 
          color="primary">
          <v-btn 
            icon 
            dark 
            @click.native="dialogAdd = false">
            <v-icon>close</v-icon>
          </v-btn>
          <v-toolbar-title>Add User</v-toolbar-title>
          <v-spacer/>
          <v-toolbar-items>
            <v-btn 
              dark 
              flat 
              @click.native="dialogAdd = false">Save</v-btn>
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
     
  </div>
</template>

<script lang="ts">
import Vue from "vue";

import ParticipantAdd from "@/components/ParticipantAdd.vue";
import ParticipantsList from "@/components/ParticipantsList.vue";
import { getAll } from "@/services/participants.service";
import { Participant } from "@/shared/models";

export default Vue.extend({
  components: {
    ParticipantAdd,
    ParticipantsList
  },

  data() {
    const participants: Participant[] = [];
    return {
      participants,
      dialogAdd: false
    };
  },

  async created() {
    this.participants = await getAll();
  }
});
</script>
