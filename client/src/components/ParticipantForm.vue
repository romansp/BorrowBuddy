<template>
  <v-form 
    ref="form"
    v-model="valid" 
    lazy-validation
    method="post" 
    @submit.prevent="submit">
    <v-text-field
      v-model="model.firstName"
      autofocus
      autocomplete="given-name"
      label="First Name"
      required
    />
    <v-text-field
      v-model="model.middleName"
      autocomplete="additional-name"
      label="Middle Name"
    />
    <v-text-field
      v-model="model.lastName"
      autocomplete="family-name"
      label="Last Name"
    />
    <v-btn
      :loading="submitting"
      type="submit"
    >
      submit
    </v-btn>
  </v-form>
</template>


<script lang="ts">
import Vue from "vue";

import { cloneDeep } from "@/common/utils";
import { add, update } from "@/services/participants.service";
import { Participant } from "@/shared/models";

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

      const { firstName, middleName, lastName, id } = this.model as Participant;
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
</script>
