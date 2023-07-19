<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" md="8" lg="6">
        <v-card>
          <v-card-title class="headline text-center">Battleship Game Login</v-card-title>
          <v-card-text>
            <v-form>
              <v-text-field v-model="username" label="User Name" id="username" required outlined></v-text-field>
              <v-alert v-if="!username && showAlertUsername" type="error" dense outlined>Write Your User Name</v-alert>
              
              <v-text-field v-model.number="boardSize" label="Board Size" type="number" required outlined></v-text-field>
              <v-alert v-if="!boardSize && showAlertBoardSize" type="error" dense outlined>Write Board Size</v-alert>
              
              <v-text-field v-model.number="shipsCount" label="Ships Count" type="number" required outlined></v-text-field>
              <v-alert v-if="!shipsCount && showAlertShipsCount" type="error" dense outlined>Write Ships Count</v-alert>
              
              <v-btn v-on:click="startGame()" color="primary">Login</v-btn>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
</style>

<script>
import axios from 'axios';

export default {
  name: 'GameLogin',
  data() {
    return {
      username: '',
      boardSize: '',
      shipsCount: '',
      showAlertUsername: false,
      showAlertBoardSize: false,
      showAlertShipsCount: false
    };
  },
  methods: {
    startGame() {
      if (!this.username) {
        this.showAlertUsername = true;
      } else {
        this.showAlertUsername = false;
      }

      if (!this.boardSize) {
        this.showAlertBoardSize = true;
      } else {
        this.showAlertBoardSize = false;
      }

      if (!this.shipsCount) {
        this.showAlertShipsCount = true;
      } else {
        this.showAlertShipsCount = false;
      }
      if (!this.username || !this.boardSize || !this.shipsCount) {
        return; 
      }
      const payload = {
        username: this.username,
        boardSize: this.boardSize,
        shipsCount: this.shipsCount,
      };
      axios.post('http://localhost:47760/api/Game/StartNewGame', payload).then((response) => {
        if (response.data == true) {
          localStorage.setItem("username", this.username);
          this.$router.push({ name: 'CurrentBoard' });
        } else {
          alert("Login is not successful");
        }
      }, (error) => {
        console.error(error);
      });
    }
  }
};
</script>
