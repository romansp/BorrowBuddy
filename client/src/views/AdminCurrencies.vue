<template>
  <div>
    <v-card>
      <ItemList 
        :items="currencies"
        :route="routeEdit"
        title="Currencies"
        item-key="code"
        item-label="code"
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
    <router-view/>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

import CurrencyAdd from "@/components/CurrencyAdd.vue";
import ItemList from "@/components/ItemList.vue";
import { routes } from "@/router";
import { getAll } from "@/services/currency.service";
import { Currency } from "@/shared/models";

export default Vue.extend({
  components: {
    CurrencyAdd,
    ItemList
  },

  data() {
    const currencies: Currency[] = [];
    return {
      currencies,
      routeAdd: routes["admin.currency.add"],
      routeEdit: routes["admin.currency.edit"],
      dialogAdd: false
    };
  },

  watch: {
    async "$route.name"(val): Promise<void | null> {
      if (val === routes["admin.currency"].name) {
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
      this.currencies = await getAll();
    }
  }
});
</script>
