<template>
  <div>
    <form
      v-if="selectedView === 'search'"
      v-on:submit="(event) => searchNames(event)"
    >
      <input type="text" v-model="searchText" placeholder="Søg Navne" />
      <button class="btn btn-primary" type="submit">Søg</button>
    </form>
    <form
      v-if="selectedView === 'suggest'"
      v-on:submit="(event) => suggestName(event)"
    >
      <input type="text" v-model="searchText" placeholder="Foreslå Navn" />
      <button type="submit">Foreslå</button>
    </form>
    <button
      v-if="selectedView === 'combo'"
      type="button"
      v-on:click="() => suggestCombo()"
    >
      Foreslå kombination
    </button>
    <select v-if="selectedView !== 'combo'" v-model="selectedSearchMethod">
      <option value="matches">Eksakt match</option>
      <option value="contains">Indeholder</option>
      <option value="startsWith">Starter med</option>
      <option value="endsWidth">Slutter med</option>
    </select>
    <select v-if="selectedView !== 'combo'" v-model="selectedCategory">
      <option value="0">Alle kategorier</option>
      <option
        v-for="category in categories"
        v-bind:key="category.id"
        v-bind:value="category.id"
      >
        {{ category?.title }}
      </option>
    </select>
    <select v-if="selectedView !== 'combo'" v-model="selectedVibration">
      <option value="0">Alle vibrationer</option>
      <option
        v-for="vibration in vibrations"
        v-bind:key="vibration.vibration"
        v-bind:value="vibration.vibration"
      >
        {{ vibration?.vibration }} - {{ vibration?.destiny }} -
        {{ vibration?.title }}
      </option>
    </select>
    <select v-if="selectedView !== 'combo'" v-model="selectedGender">
      <option value="">Alle Køn</option>
      <option value="male">Drengenavne</option>
      <option value="female">Pigenavne</option>
      <option value="both">Unisexnavne</option>
    </select>
    <input
      v-if="selectedView !== 'combo'"
      min="0"
      v-bind:max="maxLength"
      type="range"
      v-model="minLength"
    /><span v-if="selectedView !== 'combo'">{{ minLength }}</span>
    <input
      v-if="selectedView !== 'combo'"
      v-bind:min="minLength"
      max="50"
      type="range"
      v-model="maxLength"
    /><span v-if="selectedView !== 'combo'">{{ maxLength }}</span>
    <div v-if="names" class="row">
      <div
        class="col-md-4 col-lg-3"
        v-for="name in names"
        v-bind:key="name.text"
      >
        <Name v-bind:name="name" />
        <!-- <div class="card">
          <div class="card-header" v-bind:style="'background:' + (name?.gender === 'Both' ? 'lightgreen' : name?.gender === 'Male' ? 'lightblue' : 'lightpink')">
                                                        <span style="display: flex; justify-content: flex-end;">
        <i class="fa-solid" v-bind:class="name?.gender === 'Both' ? 'fa-mercury' : name?.gender === 'Male' ? 'fa-mars' : 'fa-venus'"></i></span>
                          <h2 class="card-title">{{ name?.text }}</h2>

          </div>
            <div class="card-body">
              <p class="card-text">
                <div v-if="name?.description">
                  <h4>Beskrivelse:</h4>
                  <span>{{ name?.description }}</span>      
                </div>
                <div v-if="name?.vibrationNumber">
                  <h4>Vibrationstal: {{name?.vibrationNumber?.vibration}} ({{name?.vibrationNumber?.destiny}})</h4>
                  <h5>{{name?.vibrationNumber?.title}}</h5>
                  <span>{{name?.vibrationNumber?.description }}</span>      
                </div>
              </p>
            </div>
          </div>
        </div> -->
        <!-- </div> -->
      </div>
    </div>
    <!-- <ul>
      <li v-for="name in names" v-bind:key="name.text">
        {{ name?.text }}
      </li>
    </ul> -->
  </div>
</template>

<script>
import * as helper from '../functions/nameHelper.js';
import Name from './Name.vue';

export default {
  name: 'Search',
  components: {
    Name,
  },
  data: function () {
    return {
      selectedView: 'search',
      searchText: '',
      categories: new Array(),
      names: new Array(),
      vibrations: new Array(),
      selectedSearchMethod: 'contains',
      selectedCategory: 0,
      selectedVibration: 0,
      selectedGender: '',
      minLength: 0,
      maxLength: 50,
    };
  },
  methods: {
    searchNames: function (event) {
      event.preventDefault();
      helper
        .getNames(
          this.selectedSearchMethod === 'matches' ? this.searchText : null,
          this.selectedSearchMethod === 'contains' ? this.searchText : null,
          this.selectedSearchMethod === 'startsWith' ? this.searchText : null,
          this.selectedSearchMethod === 'endsWidth' ? this.searchText : null,
          this.selectedGender,
          this.selectedVibration,
          this.maxLength,
          this.minLength,
          this.selectedCategory
        )
        .then(
          (x) => {
            this.names = x;
          },
          (e) => {
            console.log(e);
          }
        );
    },
    suggestName: function (event) {
      event.preventDefault();
      helper
        .suggestNames(
          this.selectedSearchMethod === 'matches' ? this.searchText : null,
          this.selectedSearchMethod === 'contains' ? this.searchText : null,
          this.selectedSearchMethod === 'startsWith' ? this.searchText : null,
          this.selectedSearchMethod === 'endsWidth' ? this.searchText : null,
          this.selectedGender,
          this.selectedVibration,
          this.maxLength,
          this.minLength,
          this.selectedCategory
        )
        .then(
          (x) => {
            this.names = x;
          },
          (e) => {
            console.log(e);
          }
        );
    },
    suggestCombo: function () {
      helper.suggestCombos().then(
        (x) => {
          this.names = x;
        },
        (e) => {
          console.log(e);
        }
      );
    },
  },
  mounted() {
    helper.getCategories().then(
      (x) => {
        this.categories = x.filter(
          (x) => x.title !== 'Efternavne' && x.title !== 'Titler'
        );
      },
      (e) => {
        console.log(e);
      }
    );

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
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
