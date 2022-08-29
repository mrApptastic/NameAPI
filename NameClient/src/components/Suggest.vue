<template>
  <div>
    <form class="row" v-on:submit="(event) => suggestName(event)">
      <div class="col-sm-6"> 
        <input class="form-control" type="text" v-model="suggestText" placeholder="Foresl&aring; Navn" />
      </div>
      <div class="col-sm-4">
        <select class="form-control" v-model="selectedSearchMethod">
          <option value="matches">Eksakt match</option>
          <option value="contains">Indeholder</option>
          <option value="startsWith">Starter med</option>
          <option value="endsWidth">Slutter med</option>
        </select>
      </div>
      <div class="col-sm-2">
        <button class="btn btn-primary" type="submit">Foresl&aring;</button>
      </div>            
    </form>
    <div class="row">
      <div class="col-sm-4">
        <select class="form-control" v-model="selectedCategory">
          <option value="0">Alle kategorier</option>
          <option
            v-for="category in categories"
            v-bind:key="category.id"
            v-bind:value="category.id">
            {{ category?.title }}
          </option>
        </select>
      </div>
      <div class="col-sm-4">
        <select class="form-control" v-model="selectedVibration">
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
      </div>
      <div class="col-sm-4">
        <select class="form-control" v-model="selectedGender">
          <option value="">Alle Køn</option>
          <option value="male">Drengenavne</option>
          <option value="female">Pigenavne</option>
          <option value="both">Unisexnavne</option>
        </select>
      </div>
    </div>
    <div class="row">
      <div class="col-sm-4">
        <input class="w-100"
          min="0"
          v-bind:max="maxLength"
          type="range"
          v-model="minLength"
        />
      </div>
      <div class="col-sm-2">
        <span>Min: {{ minLength }}</span>
      </div>
      <div class="col-sm-4">
        <input class="w-100"
          v-bind:min="minLength"
          max="50"
          type="range"
          v-model="maxLength"
        />
      </div>
      <div class="col-sm-2">
        <span>Max: {{ maxLength }}</span>
      </div>
    </div>
    <div v-if="searching">
      <Spinner />
    </div>
    <div v-if="names && !searching" class="row">
      <div
        class="col-md-4 col-lg-3"
        v-for="name in names"
        v-bind:key="name.text">
        <Name v-bind:name="name" />
      </div>
    </div>
  </div>
</template>

<script>
import * as helper from '../functions/nameHelper.js';
import Name from './Name.vue';
import Spinner from './Spinner.vue';

export default {
  name: 'Suggest',
  components: {
    Name,
    Spinner,
  },
  data: function () {
    return {
      searching: false, 
      suggestText: '',
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
    suggestName: function (event) {
      event.preventDefault();
      this.searching = true;
      helper
        .suggestNames(
          this.selectedSearchMethod === 'matches' ? this.suggestText : null,
          this.selectedSearchMethod === 'contains' ? this.suggestText : null,
          this.selectedSearchMethod === 'startsWith' ? this.suggestText : null,
          this.selectedSearchMethod === 'endsWidth' ? this.suggestText : null,
          this.selectedGender,
          this.selectedVibration,
          this.maxLength,
          this.minLength,
          this.selectedCategory
        )
        .then(
          (x) => {
            if (x?.length > 0 && x[0].text) {
              this.names = x;
            } else {
              this.names = new Array();
            }          
          })
        .catch((e) => {
          console.log(e);
          this.names = new Array();
        }
      ).finally(() => {
        this.searching = false;
      });
    }
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
  .row {
    margin-top: 1rem;
  }
</style>
