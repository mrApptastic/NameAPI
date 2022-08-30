<template>
<div class="container-fluid">
  <ul class="nav nav-tabs">
    <li class="nav-item">
      <a class="nav-link" v-bind:class="selectedView === 'search' ? 'active' : ''" v-on:click="selectedView = 'search'">Søg</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" v-bind:class="selectedView === 'suggest' ? 'active' : ''"  v-on:click="selectedView = 'suggest'">Foresl&aring;</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" v-bind:class="selectedView === 'combo' ? 'active' : ''"  v-on:click="selectedView = 'combo'">Kombinationer</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" v-bind:class="selectedView === 'numerology' ? 'active' : ''"  v-on:click="selectedView = 'numerology'">Numerologi</a>
    </li>
  </ul>
  <div class="container-fluid">
    <Search v-if="selectedView === 'search'" />
    <Suggest v-if="selectedView === 'suggest'" />
    <Combo v-if="selectedView === 'combo'" />
    <Numerology v-if="selectedView === 'numerology'" />
  </div>
</div>
</template>

<script>
import Search from './Search.vue';
import Suggest from './Suggest.vue';
import Combo from './Combo.vue';
import Numerology from './Numerology.vue';

export default {
  name: 'Main',
  components: {
    Search,
    Suggest,
    Combo,
    Numerology
  },
  data: function () {
    return {
      selectedView: 'search',
    };
  },
  methods: {},
  mounted() {},
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped></style>
