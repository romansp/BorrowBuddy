<template>
  <v-data-table
    :loading="loading"
    :items="items" 
    :headers="headers"
    :pagination.sync="pagination">
    <template 
      slot="items" 
      slot-scope="{ item }">
      <td>{{ item.timestamp }}</td>
      <td>{{ item.from.firstName }}</td>
      <td>{{ item.to.firstName }}</td>
      <td class="text-xs-right">{{ item.amount }}</td>
      <td>{{ item.comment }}</td>
    </template>
  </v-data-table>
</template>

<script lang="ts">
import Vue from "vue";
import { mapState } from "vuex";

import { getAll } from "@/services/flows.service";
import { Flow, Participant } from "@/shared/models";

const headers = [
  { text: "At", value: "timestamp" },
  { text: "From", value: "from" },
  { text: "To", value: "to" },
  { text: "Amount", value: "amount" },
  { text: "Comment", value: "comment" }
];

interface LogItem {
  timestamp: Date;
  from: Participant;
  to: Participant;
  amount: number;
  comment: string;
}

export default Vue.extend({
  data() {
    const items: LogItem[] = [];
    return {
      loading: false,
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

  async mounted() {
    this.fetchFlows();
  },

  methods: {
    async fetchFlows() {
      this.loading = true;
      this.items = (await getAll()).map(this.toLogItem);
      this.loading = false;
    },

    toLogItem(flow: Flow): LogItem {
      return {
        timestamp: new Date(flow.timestamp),
        from: this.participants[flow.lender],
        to: this.participants[flow.lendee],
        amount: flow.amount,
        comment: flow.comment
      };
    }
  }
});
</script>
