<template>
  <div>
    <v-card>
      <ParticipantsList 
        :items="participants"/>
      <v-card-text style="position: relative">
        <v-btn
          :to="{ name: 'admin.participants.add' }"
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
    async "$route.name"(val) {
      if (val === "admin.participants") {
        return await this.fetch();
      }
    }
  },

  created() {
    return this.fetch();
  },

  methods: {
    async fetch() {
      this.participants = await getAll();
    }
  }
});
</script>
