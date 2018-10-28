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
        <td>
          <BbDate 
            :value="props.item.timestamp" 
            relative />
        </td>
        <td>{{ props.item.from.firstName }} <v-icon class="icon-direction">arrow_right</v-icon> {{ props.item.to.firstName }}</td>
        <td class="text-xs-right"><span class="text-small">{{ props.item.flow.currencyCode }}</span><br>{{ props.item.amount }}</td>
        <td>{{ props.item.comment }}</td>
      </tr>
    </template>
    <template 
      slot="expand" 
      slot-scope="props">
      <v-card flat>
        <v-card-text>
          <v-btn 
            :loading="deleting"
            color="warning" 
            type="button"
            @click="deleteFlow(props.item.flow)">Delete</v-btn>
        </v-card-text>
      </v-card>
    </template>
  </v-data-table>
</template>

<script lang="ts">
import Vue from "vue";
import { mapState } from "vuex";

import BbDate from "@/components/BbDate.vue";
import { getAll, remove } from "@/services/flows.service";
import { Flow, Participant } from "@/shared/models";

const headers = [
  { text: "At", value: "timestamp", width: "5%" },
  { text: "From - To", value: "flow", width: "50%" },
  { text: "Amount", value: "amount", width: "5%" },
  { text: "Comment", value: "comment", width: "50%" }
];

interface LogItem {
  id: string;
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
        id: flow.id,
        timestamp: new Date(flow.timestamp),
        from,
        to,
        flow,
        flowDirection: `${from.firstName}-${to.firstName}`,
        amount: `${flow.amount / flow.currencyScale}`,
        comment: flow.comment
      };
    },

    async deleteFlow(flow: Flow) {
      this.deleting = true;
      try {
        await remove(flow.id);
        this.items = this.items.filter(item => item.id !== flow.id);
      } catch {
        this.deleting = false;
      }
    }
  }
});
</script>

<style lang="scss" scoped>
.icon-direction {
  font-size: inherit;
}
</style>
