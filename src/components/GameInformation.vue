<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" md="8" lg="6">
        <v-card class="custom-card">
          <v-card-title class="headline text-center"> <h2>Game Information: </h2> </v-card-title>
          <v-card-text>
            <ul class="game-info-list">
              <li v-for="info in gameInfo" :key="info.id">
                {{ info }}
              </li>
            </ul>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>

export default {
  name: "GameInformation",
  data() {
    return {
    };
  },
  mounted(){
    this.$emit("shotInformation", this.gameInfo);
    this.$emit("leftShips", this.leftShipsToSunk);
  },
  props: {
    gameInfo: {
      type: Array,
      required: true,
    },
    leftShipsToSunk: {
      type: Array,
      required: true
    },
  }
}

</script>

<style>
.custom-card {
  border: 4px solid rgb(0, 0, 0);
  border-radius: 10px;
}

.game-info-list {
  max-height: 120px;
  overflow-y: auto;
  list-style-type: none;
  padding: 0;
}


</style>
