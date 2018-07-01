<template>
  <v-dialog 
    v-model="opened"
    fullscreen
    hide-overlay 
    transition="dialog-bottom-transition">
    <v-card>
      <v-toolbar 
        dark 
        color="primary">
        <v-btn 
          :to="routeList" 
          exact
          icon 
          dark>
          <v-icon>close</v-icon>
        </v-btn>
        <v-toolbar-title>Edit Currency</v-toolbar-title>
        <v-spacer/>
        <v-toolbar-items>
          <v-btn 
            :loading="deleting"
            dark 
            flat
            @click="dialogRemove = true">Remove</v-btn>
          <v-dialog
            v-model="dialogRemove"
            width="500"
          >
            <v-card>
              <v-card-title
                class="headline grey lighten-2"
                primary-title
              >
                Confirm
              </v-card-title>

              <v-card-text>
                Are you sure you want to remove currency?
              </v-card-text>

              <v-divider/>

              <v-card-actions>
                <v-btn
                  color="primary"
                  flat
                  @click="dialogRemove = false"
                >
                  Cancel
                </v-btn>
                <v-spacer/>
                <v-btn
                  color="primary"
                  flat
                  @click="remove"
                >
                  Remove
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar-items>
      </v-toolbar>
      <v-card-text v-if="currency">
        <CurrencyForm 
          v-model="currency" 
          @saved="goToList" />
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from "vue";

import CurrencyForm from "@/components/CurrencyForm.vue";
import { routes } from "@/router";
import { get, remove } from "@/services/currency.service";
import { Currency } from "@/shared/models";

export default Vue.extend({
  components: {
    CurrencyForm
  },

  props: {
    id: {
      type: String,
      required: true
    }
  },

  data() {
    return {
      routeList: routes["admin.currency"],
      currency: undefined as Currency | undefined,
      deleting: false,
      opened: false,
      dialogRemove: false
    };
  },

  async created() {
    this.currency = await get(this.id);
  },

  mounted() {
    this.opened = true;
  },

  methods: {
    goToList() {
      this.$router.push(this.routeList);
    },

    async remove() {
      this.deleting = true;
      try {
        await remove(this.id);
      } catch {
        //
      }
      this.deleting = true;
      this.goToList();
    }
  }
});
</script>
