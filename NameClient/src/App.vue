<template>
  <div id="app">
    <NameSearch msg="Welcome to Your Vue.js App"/>
  </div>
</template>

<script>
import NameSearch from './components/NameSearch.vue';

export default {
  name: 'App',
  components: {
    NameSearch
  }
}
</script>

<style>
#app {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100vw;
  height: 100vh;
}
</style>
