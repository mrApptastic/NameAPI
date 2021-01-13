<template>
  <section>
    <h2>Navnes&oslash;gning:</h2>
    <input placeholder="Dette er en test uhu" type="text" width="100%" />
    <p>Hej hej</p>
    <div>
      <ul v-for="n in nameList" :key="n.text">
        {{n.text}}
      </ul>
    </div>
  </section>
</template>

<script>
import axios from 'axios';

export default {
  name: 'NameSearch',
  nameList : new Array(),
  props: {
    msg: String
  },
  mounted () {
    axios
      .get('https://localhost:5001/Names')
      .then(response => {
        console.log(response?.data);
        this.nameList = response.data;
      })
      .catch(error => {
        console.log(error)
        this.errored = true
      })
      .finally(() => this.loading = false)
  }
}
</script>

<style scoped>
  section {
    background: hotpink;
  }
</style>
