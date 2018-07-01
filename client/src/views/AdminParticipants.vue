<template>
  <div>
    <v-card>
      <ParticipantsList 
        :items="participants"/>
      <v-card-text style="position: relative">
        <v-btn
          :to="{ name: 'admin.participant.add' }"
          fixed
          dark
          fab
          bottom
          right
          color="pink"
        >
          <v-icon>add</v-icon>
        </v-btn>
      </v-card-text>
    </v-card>
    <router-view/>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

import ParticipantsList from "@/components/ParticipantsList.vue";
import { getAll } from "@/services/participants.service";
import { Participant } from "@/shared/models";

export default Vue.extend({
  components: {
    ParticipantsList
  },

  data() {
    const participants: Participant[] = [];
    return {
      participants,
      dialogAdd: false
    };
  },

  watch: {
    async "$route.name"(val): Promise<void | null> {
      if (val === "admin.participant") {
        return this.fetch();
      }
      return null;
    }
  },

  created() {
    this.fetch();
  },

  methods: {
    async fetch() {
      this.participants = await getAll();
    }
  }
});
</script>
