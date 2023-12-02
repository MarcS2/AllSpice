<template>
  <section class="row justify-content-center">
    <div class="col-6 mt-3 shadow rounded-pill">
      <section class="row">
        <div class="col-4 text-center fs-5 border-bottom border-primary border-3 rounded-left">
          My Recipes
        </div>
        <div class="col-4 text-center fs-5 ">
          <router-link :to="{ name: 'HomeChild' }" class="text-dark">
            Home
          </router-link>
        </div>
        <div class="col-4 text-center fs-5">
          <router-link :to="{ name: 'Favorite' }" class="text-dark">
            Favorites
          </router-link>
        </div>
      </section>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped>
.rounded-left {
  border-bottom-left-radius: 1rem;
}
</style>