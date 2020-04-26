/* eslint-disable max-len */
<template>
  <div id="app">
    <header class="container">
      <HeaderPage />
    </header>
    <main class="container">
      <div class="row">
        <router-view></router-view>
      </div>
    </main>
  </div>
</template>

<script>
import HeaderPage from './header/HeaderPage.vue';

export default {
  name: 'app',
  components: {
    HeaderPage,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
main {
  margin: 0 auto;
  padding: 30px;
}
</style>
