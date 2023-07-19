<template>
  <div class="container">
    <h2 class="text">Aim Your Cannon!</h2>
    <div class="input-fields">
      <v-row justify="center" align="center">
        <v-col cols="4">
          <v-text-field
            v-model.number="coordinateY"
            label="Coordinate X"
            type="number"
            outlined
            dense
          ></v-text-field>
        </v-col>
        <v-col cols="4">
          <v-text-field
            v-model.number="coordinateX"
            label="Coordinate Y"
            type="number"
            outlined
            dense
          ></v-text-field>
        </v-col>
        <v-col cols="auto">
          <v-btn color="red" @click="doAttack()">FIRE!</v-btn>
        </v-col>
      </v-row>
    </div>
  </div>
</template>

<script>

export default {
  name: "CannonShot",
  data() {
    return {
      coordinateX: null,
      coordinateY: null,
    };
  },
  methods: {
    doAttack() {
      this.$emit('attacked', this.coordinateX, this.coordinateY)
    },
  },
};
</script>

<style>
.container {
  border: 4px solid black;
  border-radius: 10px;
  padding: 16px;
  max-width: 600px;
  margin: auto;
}

.text {
  text-align: center;
  margin-bottom: 16px;
}

.input-fields {
  margin-top: 16px;
}
</style>
