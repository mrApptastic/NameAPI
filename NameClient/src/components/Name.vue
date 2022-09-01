<template>
  <div class="card">
    <div class="card-header" v-bind:style="'background:' + (name?.gender?.length > 0 ? name?.gender === 'Both' ? 'lightgreen' : name?.gender === 'Male' ? 'lightblue' : 'lightpink' : 'lightgrey')">
      <span style="display: flex; justify-content: flex-end;">
        <i v-if="name?.gender" class="fa-solid" v-bind:class="name?.gender === 'Both' ? 'fa-mercury' : name?.gender === 'Male' ? 'fa-mars' : 'fa-venus'"></i>
      </span>
      <h2 class="card-title">{{ name?.text }}</h2>
    </div>
    <div class="card-body">
      <p class="card-text">
        <span v-if="name?.description">
          <h4>Beskrivelse:</h4>
          <span v-html="name?.description"></span>      
        </span>
        <span v-if="name?.vibrationNumber">
          <h4>Vibrationstal: {{name?.vibrationNumber?.vibration}} ({{name?.vibrationNumber?.destiny}})</h4>
          <h5>{{name?.vibrationNumber?.title}}</h5>
          <span v-html="name?.vibrationNumber?.description"></span>      
        </span>
      </p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Name',
  props: {
    name: null
  },
  data: function () {
    return {

    };
  },
  methods: {

  },
  mounted() {

  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped></style>
