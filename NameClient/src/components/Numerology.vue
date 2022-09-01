<template>
  <div>
    <div class="row">
      <div class="col-sm-6">
        <label>Beregn vibrationstal:</label><br/>
        <em>Indtast dine navne</em>
        <input class="form-control" type="text" v-model="name" v-on:keyup="() => calculateVibration()" v-on:blur="() => calculateVibration()" placeholder="Beregn Navne" />
        <div v-if="nameVibrations" style="margin-top: 3rem;" class="row">
          <div
            class="col-md-6 col-lg-4"
            v-for="vibe in nameVibrations"
            v-bind:key="vibe.text">
            <Name v-bind:name="vibe" />
          </div>
        </div>
      </div>
      <div class="col-sm-6">
        <label>Beregn essens:</label><br/>
        <em>Indtast din fødselsdato</em>
        <input class="form-control" type="date" v-on:keyup="() => calculateVibration()" v-on:blur="() => calculateVibration()" v-model="birthday" />
        <label>Grundenergi: {{baseEnergy}}</label>&nbsp;<label>Månedsenergi: {{monthEnergy}}</label>&nbsp;<label>Årsenergi: {{yearEnergy}}</label>
        <div v-if="baseDescription" class="row">
          <div class="col-md-12">
            <Name v-bind:name="baseDescription" />
          </div>
        </div>
      </div>   
    </div>
  </div>
</template>

<script>
import * as helper from '../functions/nameHelper.js';
import * as number from '../functions/numberHelper.js';
import Name from './Name.vue';

export default {
  name: 'Numerology',
  components: {
    Name,
  },
  data: function () {
    return {
      name: "",
      birthday: new Date(1985, 9, 26).toISOString().slice(0, 10),
      nameVibrations: new Array(),
      baseEnergy : 0,
      baseDescription : null,
      monthEnergy: 0,
      yearEnergy: 0,
      vibrations: new Array(),
    };
  },
  methods: {
    calculateVibration: function () {
      this.calculateEnergies();

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
          text : name,
          vibrationNumber : vibe
        })
      }
      
    },
    calculateEnergies: function () {
      const d = new Date(this.birthday).toISOString();
      if (d?.length >= 10) {
        this.baseDescription = null;

        this.baseEnergy = number.calculateCharacterSum(d.slice(8, 10));
        this.monthEnergy = number.calculateCharacterSum(d.slice(5, 7));
        this.yearEnergy = number.calculateCharacterSum(d.slice(0, 4));

        const base = number.getBaseEnergy(this.baseEnergy);

        if (base) {
          this.baseDescription = {
            text: this.baseEnergy.toString(),
            description: base
          };
        }
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
