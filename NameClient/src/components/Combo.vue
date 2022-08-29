<template>
    <div>
      <div class="row">
        <div class="col-sm-12">    
          <button
            class="btn btn-primary"
            type="button"
            v-on:click="() => suggestCombo()">
            Foreslå kombination
          </button>
        </div>
        <div class="w-100" v-if="searching">
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
    </div>
  </template>
  
  <script>
  import * as helper from '../functions/nameHelper.js';
  import Name from './Name.vue';
  import Spinner from './Spinner.vue';
  
  export default {
    name: 'Combo',
    components: {
      Name,
      Spinner,
    },
    data: function () {
      return {
        searching: false,
        selectedCategory: 0,
        categories: new Array(),
        names: new Array(),
      };
    },
    methods: {
      suggestCombo: function () {
        this.searching = true;
        helper.suggestCombos().then((x) => {
          if (x?.length > 0 && x[0].text) {
            this.names = x;
          } else {
            this.names = new Array();
          }          
          }).catch((e) => {
          console.log(e);
          this.names = new Array();
        }).finally(() => {
          this.searching = false;
        });
      },
    },
    mounted() {
      helper.getCategories().then(
        (x) => {
          this.categories = x.filter(
            (x) => x.title === 'Berømte Personer' || x.title === 'Lutter Sjove Navne'
          );
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
  