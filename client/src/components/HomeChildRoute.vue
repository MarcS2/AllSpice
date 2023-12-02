<template>
  <section class="row justify-content-center">
    <div class="col-6 mt-3 shadow rounded-pill">
      <section class="row">
        <div class="col-4 text-center fs-5">
          <router-link :to="{ name: 'Recipe' }" class="text-dark">
            My Recipes
          </router-link>
        </div>
        <div class="col-4 text-center fs-5 border-bottom border-primary border-3">
          Home
        </div>
        <div class="col-4 text-center fs-5">
          <router-link :to="{ name: 'Favorite' }" class="text-dark">
            Favorites
          </router-link>
        </div>
      </section>
    </div>
  </section>
  <section class="row px-4 mt-4">
    <div v-for="recipe in recipes" :key="recipe.id" class="col-4 my-2">
      <section class="row">
        <div class="col-10">
          <section class="row rounded recipe-card-size justify-content-between"
            :style="{ backgroundImage: `url('${recipe.img}')`, backgroundPosition: 'center', backgroundSize: 'cover' }">
            <div class="col-5">
              <div class="text-light fs-5">
                <p>{{ recipe.category }}</p>
              </div>
            </div>
            <div class="col-5 text-end fs-4">
              <div v-if="!account.id">
              </div>
              <div v-else>

                <span v-if="isFavorite(recipe.id)" role="button" @click="deleteFavorite(recipe.id)">
                  <i class="mdi mdi-heart"></i>
                </span>
                <span v-else-if="isFavorite(recipe.id) == false" role="button" @click="createFavorite(recipe.id)">
                  <i class="mdi mdi-heart-outline"></i>
                </span>
              </div>
            </div>
            <div class="col-12" role="button" data-bs-toggle="modal" data-bs-target="#modalOne"
              @click="setActive(recipe.id)">
              <div>
                <p class="fs-4 text-light">{{ recipe.title }}</p>
              </div>
            </div>
          </section>
        </div>
      </section>
    </div>
  </section>
  <ModalComponent :modalId="'modalOne'" :modalSize="'modal-xl'">
    <template #modalTitle>
      <b>Modal One</b>
    </template>
    <template #modalBody>
      {{ active.value }}
    </template>
  </ModalComponent>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch, ref } from 'vue';
import { logger } from "../utils/Logger";
import { recipesService } from '../services/RecipesService'
import { favoritesService } from '../services/FavoritesService'
import ModalComponent from "./ModalComponent.vue";
import Pop from "../utils/Pop";
import { api } from "../services/AxiosService";
export default {
  setup() {
    onMounted(() => {
      getRecipes()
    })
    watch(() => AppState.account.id, () => {
      getMyFavorites()
    })
    let active = ref({});

    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getMyFavorites() {
      try {
        await favoritesService.getMyFavorites()
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      active,
      recipes: computed(() => AppState.recipes),
      favorites: computed(() => AppState.favorites),
      account: computed(() => AppState.account),

      async createFavorite(recipeId) {
        try {
          await favoritesService.createFavorite(recipeId)
        } catch (error) {
          Pop.error(error)
        }
      },

      async deleteFavorite(recipeId) {
        try {
          const foundFavorite = this.favorites.find(favorite => favorite.recipeId == recipeId)
          const favoriteId = foundFavorite.id
          const message = await favoritesService.deleteFavorite(favoriteId)
        } catch (error) {
          Pop.error(error)
        }
      },

      isFavorite(recipeId) {
        return this.favorites.some(favorite => favorite.recipeId == recipeId)
      },

      setActive(recipeId) {
        this.active.value = this.recipes.find(recipe => recipe.id == recipeId)
        logger.log(active)
      }
    }
  },
  components: { ModalComponent }
};
</script>


<style lang="scss" scoped>
.recipe-card-size {
  min-height: 25dvh;
}
</style>