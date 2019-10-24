<template>
  <v-list subheader>
    <v-subheader>{{ title }}</v-subheader>
    <v-list-item 
      v-for="item in items" 
      :key="item[itemKey]" 
      :to="{ name: route.name, params: { id: item[itemKey] } }" 
      avatar
    >
      <v-list-item-content>
        <v-list-item-title v-text="item[itemLabel]" />
      </v-list-item-content>
      <v-list-item-action>
        <v-icon>edit</v-icon>
      </v-list-item-action>
    </v-list-item>
  </v-list>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    items: {
      type: Array,
      default: () => []
    },

    title: {
      type: String,
      required: false,
      default: ""
    },

    route: {
      type: Object,
      required: false,
      default: () => ({})
    },

    itemKey: {
      type: String,
      required: false,
      default: "id"
    },

    itemLabel: {
      type: String,
      required: false,
      default: "name"
    }
  }
});
</script>
