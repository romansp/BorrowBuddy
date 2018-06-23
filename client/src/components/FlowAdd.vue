<template>
  <form 
    method="post" 
    @submit.prevent="submit">
    <div>
      From: <select 
        v-model="from" 
        name="from">
        <option 
          v-for="participant in participants" 
          :key="participant.id" 
          :value="participant.id">{{ participant.firstName }}</option>
      </select>
    </div>
    <div>
      To: <select 
        v-model="to" 
        name="to">
        <option 
          v-for="participant in participants" 
          :key="participant.id" 
          :value="participant.id">{{ participant.firstName }}</option>
      </select>
    </div>
    <div>
      <button 
        type="button" 
        @click="swap">Swap</button>
    </div>
    <div>
      Total: <input 
        v-model.number="amount" 
        type="number" 
        name="amount">
    </div>
    <div>
      <Balance 
        :from="from" 
        :to="to" />
    </div>
    <div> 
      Comment: <input 
        v-model="comment" 
        type="text" 
        name="comment">
    </div>
    <button type="submit">OK</button>
  </form>
</template>


<script lang="ts">
import Vue from "vue";
import { mapState } from "vuex";

import { add, getAll } from "../services/flows.service";
import Balance from "./Balance.vue";

export default Vue.extend({
  components: {
    Balance
  },

  data() {
    return {
      amount: 0,
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
    }
  }
});
</script>