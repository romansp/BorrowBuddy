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
      Total: <input 
        v-model.number="amount" 
        type="number" 
        name="amount">
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
import Vue from 'vue';
import { mapState } from 'vuex';
import { FlowsService } from '../services/flows.service';
export default Vue.extend({
  data() {
    return {
      amount: 0,
      from: '',
      to: '',
      comment: ''
    };
  },

  computed: mapState<any>({
    participants: state => state.participants
  }),

  async mounted() {
    await this.$store.dispatch('PARTICIPANTS_FETCH');
  },

  methods: {
    async submit() {
      const { amount, from, to, comment } = this;
      await FlowsService.post({
        amount,
        from,
        to,
        comment
      });
    }
  }
});
</script>
