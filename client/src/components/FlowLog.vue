<template>
  <v-data-table
    :loading="loading"
    :items="items" 
    :headers="headers"
    :pagination.sync="pagination">
    <template 
      slot="items" 
      slot-scope="{ item }">
      <td><BbDate 
        :value="item.timestamp" 
        relative /></td>
      <td>{{ item.from.firstName }} <v-icon class="icon-direction">arrow_right</v-icon> {{ item.to.firstName }}</td>
      <td class="text-xs-right"><span class="text-small">{{ item.flow.currencyCode }}</span><br>{{ item.amount }}</td>
      <td>{{ item.comment }}</td>
    </template>
  </v-data-table>
</template>

<script lang="ts">
import Vue from "vue";
import { mapState } from "vuex";

import BbDate from "@/components/BbDate.vue";
import { getAll } from "@/services/flows.service";
import { Flow, Participant } from "@/shared/models";

const headers = [
  { text: "At", value: "timestamp", width: "5%" },
  { text: "From - To", value: "flow", width: "50%" },
  { text: "Amount", value: "amount", width: "5%" },
  { text: "Comment", value: "comment", width: "50%" }
];

interface LogItem {
  timestamp: Date;
  from: Participant;
  to: Participant;
  flow: Flow;
  flowDirection: string;
  amount: string;
  comment: string;
}

export default Vue.extend({
  components: {
    BbDate
  },

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
      const from: Participant = this.participants[flow.lender];
      const to: Participant = this.participants[flow.lendee];
      return {
        timestamp: new Date(flow.timestamp),
        from,
        to,
        flow,
        flowDirection: `${from.firstName}-${to.firstName}`,
        amount: `${flow.amount / flow.currencyScale}`,
        comment: flow.comment
      };
    }
  }
});
</script>

<style lang="scss" scoped>
.icon-direction {
  font-size: inherit;
}
</style>
