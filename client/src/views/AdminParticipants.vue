<template>
  <div>
    <ParticipantAdd />
  </div>
</template>

<script lang="ts">
import Vue from "vue";

import ParticipantAdd from "@/components/ParticipantAdd.vue";

export default Vue.extend({
  components: {
    ParticipantAdd
  }
});
</script>
