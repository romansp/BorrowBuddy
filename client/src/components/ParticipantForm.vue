<template>
  <v-form 
    ref="form"
    v-model="valid" 
    method="post" 
    @submit.prevent="submit">
    <v-text-field
      v-model="model.firstName"
      label="First Name"
      required
    />
    <v-text-field
      v-model="model.middleName"
      label="Middle Name"
    />
    <v-text-field
      v-model="model.lastName"
      label="Last Name"
    />
    <v-btn
      :loading="submitting"
      @click="submit"
    >
      submit
    </v-btn>
  </v-form>
</template>


<script lang="ts">
import Vue from "vue";

import { add, update } from "@/services/participants.service";

export default Vue.extend({
  props: {
    value: {
      type: Object,
      required: true
    }
  },

  data() {
    return {
      valid: false,
      submitting: false,
      model: cloneDeep(this.value)
    };
  },

  methods: {
    async submit() {
      this.submitting = true;

      const { firstName, middleName, lastName, id } = this.model;
      const model = {
        id,
        firstName,
        lastName,
        middleName
      };

      try {
        if (id) {
          await update(id, model);
        } else {
          await add(model);
        }
      } catch {
        //
      }

      this.submitting = false;
      this.$emit("saved", true);
    }
  }
});

function cloneDeep<T>(obj: any): T {
  return JSON.parse(JSON.stringify(obj));
}
</script>
