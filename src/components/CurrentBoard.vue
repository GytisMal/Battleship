<template>
  <div>
    <h1>Hello, Captain {{ this.userName }}! </h1>
    <img :src="saluteGif" alt="Salute Gif" class="saluteGif" width="200" height="200">
    <GameInformation :game-info="gameInfo" @shotInformation="gameInformation" :left-ships="leftShipsToSunk" @leftShips="gameInformation"/>
    <div class="boardContainer">
      <div>
        <table>
          <tbody>
            <tr v-for="(row, i) in board" v-bind:key="i">
              <td v-for="(cell, j) in row" v-bind:key="j">
                <div class="cube">
                  <img v-if="cell.Type === 1" :src="missedHit" alt="MissedHit" class="missedHitImg" width="30" height="30">
                  <img v-else-if="cell.Type == 2" :src="shipsIcon" alt="shipsIcon" class="shipsImg" width="70" height="70">
                  <img v-else-if="cell.Type == 3" :src="hitedShips" alt="HitedShip" class="hitedShipsImg" width="30" height="30">
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <br />
   <CannonShot @attacked="makeAttack" />
  </div>
  <div v-show="allShipsFound" class="game-end-alert">
    <div class="alert">
      You Destroyed All Pirates Ships, Congratulations!
      <button @click="navigateToLogin">Click Here To Go Back To Login Page</button>
    </div>
  </div>
</template>

<style scoped>

.game-end-alert {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: rgb(200, 200, 200);
}

.game-end-alert .alert {
  background-color: rgb(200, 200, 200);
  padding: 20px;
  text-align: center;
}
.game-end-alert button {
  margin-top: 20px;
}
.cube {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 50px;
  height: 50px;
  background: #05c0fe;
  text-align: center;
}

.hitedShipsImg {
  display: block;
  align-items: center;
  height: auto;
  width: 75%;
  max-width: 100%;
  max-height: 100%;
  display: flex;
}

.seaImg,
.missedHitImg,
.shipsImg {
  display: block;
  width: 100%;
  height: auto;
}

.saluteGif {
  display: flex;
  justify-content: center;
  flex-direction: column;
  margin-top: 50px;
}

table {
  margin: auto;
  text-align: center; 
  border: 60px solid;
  border-color: #f4c88e;
}

</style>

<script>
import axios from "axios";
import SaluteGif from "@/gifs/salute.gif";
import Ships from "@/gifs/ShipsIcon.svg";
import HitedShips from "@/gifs/ShipHit.svg";
import MissedHit from "@/gifs/EmptyHit.svg";
import CannonShot from './CannonShot.vue';
import GameInformation from './GameInformation.vue';


export default {
  name: 'CurrentBoard',
  components: { CannonShot, GameInformation },
  data() {
  return {
    userName: localStorage.getItem("username"),
    saluteGif: SaluteGif,
    shipsIcon: Ships,
    hitedShips: HitedShips,
    missedHit: MissedHit,
    board: [],
    gameInfo: [],
    leftShips: [],
    gameEnded: false,
   }
  },
  computed: {
  allShipsFound() {
    // return this.board.every(row => row.every(cell => cell.Type === 1 || cell.Type === 3)); // veikia
    // return this.board.every(shipsCount = shipsCount.every(cell => cell.Type === 3));
     return this.leftShips == 0; //Kai ShipsCount bus nulis, tada gameEnded turi buti true
  }
},
  watch: {
    allShipsFound: {
    immediate: true,
    handler(newValue) {
      if (newValue) {
        this.gameEnded = true;
      }
    }
  }
},
    methods: {
      navigateToLogin() {
        this.$router.push('/GameLogin');
      },
      getCurrentBoard() {
        axios.get('http://localhost:47760/api/Game/GetUserGame?userName=' + this.userName).then((response) => {
          if(response.data && response.data.CurrentBoard && response.data.CurrentBoard.length > 0) {
            this.board = response.data.CurrentBoard;
          }
          this.leftShips = response.data.ShipsCount;
        }, (error) => {
          console.error(error);
        });
      },
      makeAttack(coordinateX, coordinateY) {
        const payload =  {
            userName: this.userName,
            X: coordinateX,
            Y: coordinateY,
        } 
        axios.post('http://localhost:47760/api/Game/Attack', payload).then((response) => {
          if(response.data.CellType) {
            this.board[coordinateX][coordinateY].Type = response.data.CellType;
          } 
          if(response.data.Information){
            this.gameInfo.unshift(response.data.Information);
          }
          this.leftShips = response.data.Ships;
        }) 
        .catch((error) => {
        console.error(error);
        });
      },
    },
    created() {
      this.getCurrentBoard();
    },
};
</script>