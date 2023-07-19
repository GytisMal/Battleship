<template>
  <div>
    <router-view v-if="$route.name !== 'GameLogin'"/>
    <GameLogin v-else/>
  </div>
</template>

<script>
import GameLogin from "./components/GameLogin.vue";

export default {
  components: {
    GameLogin,
  },
};
</script>
