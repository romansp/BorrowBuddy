<template>
  <v-list subheader>
    <v-subheader>Users</v-subheader>
    <v-list-tile 
      v-for="item in items" 
      :key="item.id" 
      :to="{name: 'admin.participants.edit', params: { id: item.id }}" 
      avatar>
      <v-list-tile-content>
        <v-list-tile-title v-text="item.firstName"/>
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
    }
  }
});
</script>
