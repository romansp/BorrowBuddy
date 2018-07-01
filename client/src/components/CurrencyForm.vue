<template>
  <v-form 
    ref="form"
    v-model="valid" 
    method="post" 
    @submit.prevent="submit">
    <v-text-field
      v-model="model.code"
      autofocus
      label="Code"
      required
    />
    <v-text-field
      v-model="model.symbol"
      label="Symbol"
    />
    <v-text-field
      v-model="model.scale"
      type="number"
      label="Scale"
    />
    <v-btn
      :loading="submitting"
      type="submit"
      @click="submit"
    >
      submit
    </v-btn>
  </v-form>
</template>


<script lang="ts">
import Vue from "vue";

import { cloneDeep } from "@/common/utils";
import { add, update } from "@/services/currency.service";
import { Currency } from "@/shared/models";

export default Vue.extend({
  props: {
    value: {
      type: Object,
      required: true
    }
  },

  data() {
    const shouldUpdate = this.value.code !== "" ? true : false;
    return {
      shouldUpdate,
      valid: false,
      submitting: false,
      model: cloneDeep(this.value)
    };
  },

  methods: {
    async submit() {
      this.submitting = true;

      const { code, symbol, scale } = this.model as Currency;
      const model = {
        code,
        symbol,
        scale
      };

      try {
        if (this.shouldUpdate) {
          await update(code, model);
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
