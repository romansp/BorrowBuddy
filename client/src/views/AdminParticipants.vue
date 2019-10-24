<template>
  <div>
    <v-card>
      <ItemList 
        :items="participants"
        :route="routeEdit"
        title="Users"
        item-key="id"
        item-label="firstName"
      />
      <v-card-text style="position: relative">
        <v-btn
          :to="routeAdd"
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
    <router-view />
  </div>
</template>

<script lang="ts">
import Vue from "vue";

import ItemList from "@/components/ItemList.vue";
import { routes } from "@/router";
import { getAll } from "@/services/participants.service";
import { Participant } from "@/shared/models";

export default Vue.extend({
  components: {
    ItemList
  },

  data() {
    const participants: Participant[] = [];
    return {
      participants,
      routeAdd: routes["admin.participant.add"],
      routeEdit: routes["admin.participant.edit"],
      dialogAdd: false
    };
  },

  watch: {
    // eslint-disable-next-line object-shorthand
    async "$route.name"(val): Promise<void | null> {
      if (val === routes["admin.participant"].name) {
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
