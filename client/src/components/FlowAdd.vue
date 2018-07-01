<template>
  <form 
    method="post" 
    @submit.prevent="submit">
    <v-text-field
      v-model.number="amount"
      autofocus
      type="number"
      label="Amount"
    />
    <v-layout 
      row 
    >
      <v-flex xs6>
        <v-select 
          v-model="from"
          :items="skip(participants, to)"
          name="from"
          item-text="firstName"
          item-value="id"
          label="From"/>
      </v-flex>
      <v-flex class="swap-button">
        <v-btn 
          type="button"
          title="Swap" 
          @click="swap">
          <v-icon>swap_horiz</v-icon>
        </v-btn>
      </v-flex>
      <v-flex xs6>
        <v-select 
          v-model="to" 
          :items="skip(participants, from)"
          name="To"
          item-text="firstName"
          item-value="id"
          label="To"/>
      </v-flex>
    </v-layout>
    <div>
      <Balance 
        :from="from" 
        :to="to" />
    </div>
    <v-textarea
      v-model="comment" 
      placeholder="Comment"
      auto-grow
      rows="1"
    />
    <v-btn type="submit">OK</v-btn>
  </form>
</template>


<script lang="ts">
import Vue from "vue";
import { mapState } from "vuex";

import { Participant } from "@/shared/models";
import { add, getAll } from "../services/flows.service";
import Balance from "./Balance.vue";

export default Vue.extend({
  components: {
    Balance
  },

  data() {
    return {
      amount: undefined,
      from: "",
      to: "",
      comment: ""
    };
  },

  computed: mapState<any>({
    participants: state => state.participants
  }),

  async mounted() {
    await this.$store.dispatch("PARTICIPANTS_FETCH");
  },

  methods: {
    async submit() {
      const { amount, from, to, comment } = this;
      if (!amount) {
        return;
      }
      await add({
        amount,
        lender: from,
        lendee: to,
        comment,
        currencyCode: "BYN"
      });
    },

    swap() {
      const temp = this.from;
      this.from = this.to;
      this.to = temp;
    },

    skip(items: Participant[], selected: string) {
      return items.filter(item => item.id !== selected);
    }
  }
});
</script>

<style lang="scss" scoped>
.swap-button {
  flex: 0 1 auto;
}
</style>
