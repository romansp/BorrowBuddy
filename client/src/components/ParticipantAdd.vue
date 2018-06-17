<template>
  <div>
    <form 
      method="post" 
      @submit.prevent="submit">
      <div>
        First Name:
        <input 
          v-model="firstName" 
          type="text" 
          name="Code">
      </div>
      <div>
        Middle Name:
        <input 
          v-model="middleName" 
          type="text" 
          name="Symbol">
      </div>
      <div>
        Last Name:
        <input 
          v-model="lastName" 
          type="text" 
          name="Symbol">
      </div>
      <button type="submit">OK</button>
    </form>
  </div>
</template>


<script lang="ts">
import Vue from "vue";

import { add } from "@/services/participants.service";

export default Vue.extend({
  data() {
    return {
      firstName: "",
      middleName: "",
      lastName: ""
    };
  },

  methods: {
    async submit() {
      const { firstName, middleName, lastName } = this;
      await add({
        firstName,
        middleName,
        lastName
      });
    }
  }
});
</script>
