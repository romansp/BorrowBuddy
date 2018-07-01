<template>
  <div>
    <v-card>
      <ItemList 
        :items="currencies"
        title="Currencies"
        item-key="code"
        item-label="code"
        route="admin.currency.edit"
      />
      <v-card-text style="position: relative">
        <v-btn
          :to="{ name: 'admin.currency.add' }"
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
      dialogAdd: false
    };
  },

  watch: {
    async "$route.name"(val): Promise<void | null> {
      if (val === "admin.currency") {
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
