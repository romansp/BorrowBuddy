<template>
  <v-list subheader>
    <v-subheader>{{ title }}</v-subheader>
    <v-list-tile 
      v-for="item in items" 
      :key="item[itemKey]" 
      :to="{ name: route, params: { id: item[itemKey] } }" 
      avatar>
      <v-list-tile-content>
        <v-list-tile-title v-text="item[itemLabel]"/>
      </v-list-tile-content>
      <v-list-tile-action>
        <v-icon>edit</v-icon>
      </v-list-tile-action>
    </v-list-tile>
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
      type: String,
      required: false,
      default: ""
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
