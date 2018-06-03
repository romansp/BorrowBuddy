<template>
  <div class="home">
    <img src="../assets/logo.png">
    <BAddFlow />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import BAddFlow from '@/components/BAddFlow.vue';

@Component({
  components: {
    BAddFlow
  }
})
export default class Home extends Vue {}
</script>
