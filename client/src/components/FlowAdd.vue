<template>
  <form 
    method="post" 
    @submit.prevent="submit">
    <v-layout 
      row 
    >
      <v-flex shrink>
        <v-select 
          v-model="currency"
          :items="currencies"
          required
          return-object
          name="from"
          item-text="code"
          label="Currency"/>
      </v-flex>
     
      <v-text-field
        v-model.number="amount"
        :step="currencyScale"
        autofocus
        type="number"
        label="Amount"
      />
    </v-layout>
    <v-layout 
      row 
    >
      <v-flex xs6>
        <v-select 
          v-model="from"
          :items="skip(participants, to)"
          required
          name="from"
          item-text="firstName"
          item-value="id"
          label="From"
          @change="updateBalance"/>
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
          required
          name="To"
          item-text="firstName"
          item-value="id"
          label="To"
          @change="updateBalance"/>
      </v-flex>
    </v-layout>
    <div>
      <Balance 
        :amount="balance.amount * currencyScale" />
    </div>
    <v-textarea
      v-model="comment" 
      placeholder="Comment"
      auto-grow
      rows="1"
    />
    <v-btn 
      :loading="submitting" 
      type="submit">OK</v-btn>
  </form>
</template>


<script lang="ts">
import Vue from "vue";
import { mapState } from "vuex";

import { Currency, Participant } from "@/shared/models";
import { add } from "../services/flows.service";
import Balance from "./Balance.vue";

export default Vue.extend({
  components: {
    Balance
  },

  data() {
    return {
      currency: undefined as Currency | undefined,
      amount: undefined,
      submitting: false,
      from: "",
      to: "",
      comment: ""
    };
  },

  computed: {
    ...mapState<any>({
      participants: state => Object.values(state.participants),
      currencies: state => Object.values(state.currencies),
      balance: state => state.balance
    }),
    currencyScale(): number {
      const currency = this.currency as Currency | undefined;
      return currency ? 1 / currency.scale : 100;
    }
  },

  methods: {
    async updateBalance() {
      const { from, to, currency } = this;
      if (from && to && currency) {
        await this.$store.dispatch("BALANCE_FETCH", {
          from,
          to,
          code: currency.code
        });
      }
    },

    async submit() {
      const { amount, from, to, comment, currency } = this;
      if (!amount || !from || !to || !currency) {
        return;
      }
      await add({
        amount: amount * currency.scale,
        lender: from,
        lendee: to,
        comment,
        currencyCode: currency.code
      });
      await this.updateBalance();
    },

    swap() {
      const temp = this.from;
      this.from = this.to;
      this.to = temp;
      this.updateBalance();
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
