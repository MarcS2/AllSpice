<template>
  <div class="container-fluid">
    <section class="row justify-content-center mt-3 px-3">
      <div class="col-12 hero-bg shadow rounded">

      </div>
      <div class="col-12">
        <router-view></router-view>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import { logger } from "../utils/Logger";
import { ref } from "vue";
import { computeStyles } from "@popperjs/core";
import { AppState } from "../AppState";

export default {
  setup() {
    const account = computed(() => AppState.account)
    const route = useRoute()
    const router = useRouter()
    onMounted(() => {
      checkRoute()
    })

    function checkRoute() {
      if (route.fullPath == '/favorite' || '/recipe' && !account.id) {
        router.push('/')
      }
    }
    return {
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.hero-bg {
  background-image: url('https://images.unsplash.com/photo-1583330613963-19051a45ee4c?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D');
  background-size: cover;
  background-position: center;
  height: 35dvh;

}
</style>
