<template>
<div class="container-fluid">
  <ul class="nav nav-tabs">
    <li class="nav-item">
      <a class="nav-link" v-bind:class="selectedView === 'search' ? 'active' : ''" v-on:click="selectedView = 'search'">Søg</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" v-on:click="selectedView = 'suggest'">Foresl&aring;</a>
    </li>
    <li class="nav-item">
      <a class="nav-link" v-on:click="selectedView = 'combo'">Kombinationer</a>
    </li>
    <li class="nav-item">
      <a class="nav-link disabled" v-on:click="selectedView = 'numerology'">Numerologi</a>
    </li>
  </ul>
  <div class="container-fluid">
    <Search />
  </div>

</div>
</template>

<script>
import Search from './Search.vue';

export default {
  name: 'Main',
  components: {
    Search,
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
