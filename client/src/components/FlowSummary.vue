<template>
  <v-data-table
    :loading="loading"
    :items="items"
    :headers="headers" 
    :pagination.sync="pagination"
    item-key="id">
    <template 
      slot="items" 
      slot-scope="props">
      <tr @click="props.expanded = !props.expanded">
        <td>{{ props.item.participantName }} <v-icon class="icon-direction">arrow_right</v-icon> {{ props.item.amount }}</td>
      </tr>
    </template>
    <template 
      slot="expand" 
      slot-scope="props">
      <v-card flat>
        <v-card-text 
          v-for="(flow, index) in props.item.flows" 
          :key="index">
          {{ flow.currencyCode }} <v-icon class="icon-direction">arrow_right</v-icon> {{ flow.amount }}
        </v-card-text>
      </v-card>
    </template>
  </v-data-table>
</template>

<script lang="ts">
import Vue from "vue";
import { mapState } from "vuex";

import { getAll } from "@/services/flows.service";
import { Flow, Participant } from "@/shared/models";

interface SummaryItem {
  participantName: string;
  amount: number;
  flows: Array<{
    participant: Participant;
    currencyCode: string;
    amount: string;
  }>;
}

const headers = [{ text: "Participant", value: "participantName" }];

export default Vue.extend({
  data() {
    const items: SummaryItem[] = [];
    return {
      loading: false,
      deleting: false,
      items,
      headers,
      pagination: {
        rowsPerPage: -1,
        descending: true
      }
    };
  },

  computed: {
    ...mapState<any>({
      participants: state => state.participants,
      currencies: state => state.currencies
    })
  },

  mounted() {
    this.fetchFlows();
  },

  methods: {
    async fetchFlows() {
      this.loading = true;
      this.items = (await getAll()).map(this.toSummaryItem);
      this.loading = false;
    },

    toSummaryItem(_: Flow): SummaryItem {
      return {
        participantName: "",
        amount: 0,
        flows: []
      };
    }
  }
});
</script>
