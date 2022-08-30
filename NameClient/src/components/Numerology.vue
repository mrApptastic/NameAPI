<template>
  <div>
    <div class="row">
      <div class="col-sm-12">
        <input class="form-control" type="text" v-model="name" v-on:change="() => calculateVibration()" placeholder="Beregn Navne" />
        {{vibration}}
      </div>   
    </div>
  </div>
</template>

<script>
import * as helper from '../functions/nameHelper.js';
import * as number from '../functions/numberHelper.js';

export default {
  name: 'Numerology',
  components: {

  },
  data: function () {
    return {
      name: "",
      vibration: 0,
      vibrations: new Array(),
    };
  },
  methods: {
    calculateVibration: function () {
      this.vibration = number.calculateNameVibration(this.name);
    }
  },
  mounted() {
    helper.getVibrations().then(
      (x) => {
        this.vibrations = x
          .filter((x) => x.vibration > 9)
          .sort(function (a, b) {
            return a.vibration - b.vibration || a.destiny - b.destiny;
          });
      },
      (e) => {
        console.log(e);
      }
    );
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .row {
    margin-top: 1rem;
  }
</style>
