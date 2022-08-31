<template>
  <div>
    <div class="row">
      <div class="col-sm-6">
        <input class="form-control" type="text" v-model="name" v-on:change="() => calculateVibration()" placeholder="Beregn Navne" />
        {{nameVibrations}}
      </div>
      <div class="col-sm-6">
        <input class="form-control" type="date" v-on:change="() => calculateEnergies()" v-model="birthday" />
        {{birthday}}<br/>
        {{baseEnergy}}
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
      birthday: new Date().toISOString().slice(0, 10),
      nameVibrations: new Array(),
      baseEnergy : 0,
      monthEnergy: 0,
      yearEnergy: 0,
      vibrations: new Array(),
    };
  },
  methods: {
    calculateVibration: function () {
      this.nameVibrations = new Array();

      const nameList = this.name.split(" ");

      for (const name of nameList) {
        let vib = number.calculateNameVibration(name);

        if (vib < 10) {
          vib += this.baseEnergy;
        }

        if (vib < 10) {
          vib += 9;
        }

        const vibe = this.vibrations.find(x => x.vibration === vib);

        this.nameVibrations.push({
          name : name,
          vibration : vibe
        })
      }
      
    },
    calculateEnergies: function () {
      const d = new Date(this.birthday).toISOString();
      if (d?.length >= 10) {
        this.baseEnergy = number.calculateCharacterSum(d.slice(8, 10));
        this.monthEnergy = number.calculateCharacterSum(d.slice(5, 7));
        this.yearEnergy = number.calculateCharacterSum(d.slice(0, 4));
      }      
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
