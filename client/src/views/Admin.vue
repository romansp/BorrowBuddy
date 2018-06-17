<template>
  <div class="admin">
    <h1>This is an about page</h1>
    <div id="nav">
      <router-link to="/admin/currencies">Currencies</router-link> |
      <router-link to="/admin/participants">Participants</router-link>
    </div>
    <router-view/>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({});
</script>
