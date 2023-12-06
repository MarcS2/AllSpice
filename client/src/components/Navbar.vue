<template>
  <nav class="row navbar navbar-expand-sm navbar-dark px-3 ">
    <div class="col-12">
      <section class="row justify-content-between">
        <div class="col-3">
          <div class="p-2  mt-2 d-flex align-items-center">
            <div class="">
              <input @keyup.enter="getRecipesByQuery()" v-model="search" class="rounded-pill " type="text"
                placeholder="Search By Category">
            </div>
            <div>
              <i @click="getRecipesByQuery()" class="mdi mdi-magnify fs-3 " title="search" role="button"></i>
            </div>
          </div>
        </div>


        <div class="col-3">
          <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
            aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
        </div>

        <div class=" col-3 collapse navbar-collapse" id="navbarText">
          <ul class="navbar-nav me-auto">
            <li>
              <!-- <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
                About
              </router-link> -->
            </li>
          </ul>
          <!-- LOGIN COMPONENT HERE -->
          <div class="me-4 mt-1">
            <button class="btn btn-outline-primary">New Recipe +</button>
          </div>
          <div>
            <button class="btn text-light" @click="toggleTheme"><i class="mdi"
                :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button>
          </div>
          <Login />
        </div>
      </section>
    </div>
  </nav>
  <section class="row ">
    <div class="col-12" v-if="search != ''" v-for="category in filterRecipes()" :key="category">
      <div class="ms-4 d-flex">
        <div class="rounded mb-1" style="background-color: rgba(0, 0, 0, 0.368); ">
          <p class="mb-0">{{ category }}</p>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { recipesService } from "../services/RecipesService";
import { logger } from "../utils/Logger";
import { computeStyles } from "@popperjs/core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import ModalComponent from "./ModalComponent.vue";
export default {
  setup() {
    const search = ref('')
    const theme = ref(loadState('theme') || 'light')
    const categories = [
      "specialty coffee",
      "cheese",
      "soup",
      "italian",
      "mexican"
    ]
    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      categories,
      search,
      theme,
      recipes: computed(() => AppState.recipes),
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      },

      async getRecipesByQuery() {
        try {
          const query = search.value
          await recipesService.getRecipesByQuery(query)
        } catch (error) {
          Pop.error(error)
        }
      },

      filterRecipes() {
        return categories.filter(category => category.toLowerCase().includes(search.value.toLocaleLowerCase()))
      }
    }
  },
  components: { Login, ModalComponent }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

/* .navbar {
  position: absolute;
} */


.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
