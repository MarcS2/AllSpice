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
                <span class="d-flex ">
                  <p class="blur-bg rounded p-1">{{ recipe.category }}</p>
                </span>
              </div>
            </div>
            <div class="col-5 text-end fs-4">
              <div v-if="!account.id">
              </div>
              <div v-else class="mt-2">

                <span v-if="isFavorite(recipe.id)" class=" blur-bg rounded p-2 px-3" role="button"
                  @click="deleteFavorite(recipe.id)">
                  <i class="mdi mdi-heart"></i>
                </span>
                <span v-else-if="isFavorite(recipe.id) == false" class="blur-bg rounded p-2 px-3" role="button"
                  @click="createFavorite(recipe.id)">
                  <i class="mdi mdi-heart-outline"></i>
                </span>
              </div>
            </div>
            <div class="col-12" role="button" data-bs-toggle="modal" :data-bs-target="getModalId(recipe.id)"
              @click="setActive(recipe.id)">
              <div>
                <span class="d-flex">
                  <p class="fs-4 text-light blur-bg rounded">{{ recipe.title }}</p>
                </span>
              </div>
            </div>
          </section>
        </div>
      </section>
    </div>
  </section>
  <ModalComponent v-for="recipe in  recipes " :key="recipe.id" :modalId="'modal_' + recipe.id.toString()"
    :modalSize="'modal-xl'">
    <template #modalTitle>
      <b>Modal One</b>
    </template>
    <template #modalBody>
      <div class="container-fluid">
        <section class="row">
          <div class="col-5">
            <section class="row justify-content-start"
              :style="{ backgroundImage: `url('${recipe.img}')`, backgroundPosition: 'center', backgroundSize: 'cover', minHeight: '35dvh' }">
              <div class="col-1 text-start fs-4 mt-2">
                <div v-if="!account.id">
                </div>
                <div v-else>

                  <span v-if="isFavorite(recipe.id)" class="blur-bg rounded p-2 px-3 " role="button"
                    @click="deleteFavorite(recipe.id)">
                    <i class="mdi mdi-heart"></i>
                  </span>
                  <span v-else-if="isFavorite(recipe.id) == false" class="blur-bg rounded p-2 px-3" role="button"
                    @click="createFavorite(recipe.id)">
                    <i class="mdi mdi-heart-outline"></i>
                  </span>
                </div>
              </div>
            </section>
          </div>
          <div class="col-7">
            <section class="row">
              <div class="col-12 text-end mt-2">
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="col-12">
                <p class=" fs-3 d-inline me-3">
                  {{ recipe.title }}
                </p>
                <p class="d-inline blur-bg p-1 rounded-pill">{{ recipe.category }}</p>
              </div>
              <div class="col-6">
                <InstructionComponent>
                  <template #title>
                    <p class="mb-0 text-light">Instructions</p>
                  </template>
                  <template #instructions>
                    <p class="text-start">{{ recipe.instructions }}</p>
                  </template>
                </InstructionComponent>
              </div>
              <div class="col-6">
                <InstructionComponent>
                  <template #title>
                    <p class="mb-0 text-light">Ingredients</p>
                  </template>
                  <template #instructions>
                    <div v-for="activeIngredient in activeIngredients" :key="activeIngredient.id">
                      <p class="fs-5">
                        {{ activeIngredient.quantity + " " + activeIngredient.name + "," }}
                      </p>
                    </div>
                    <form @submit.prevent="createIngredient(recipe.id)" class="row mt-2">
                      <div class="col-6 mb-3 text-start">
                        <label :for="'floatingInput' + recipe.id" class=" fs-6">Name</label>
                        <input v-model="editable.name" type="email" class="form-control "
                          :id="'floatingInput' + recipe.id" placeholder="Ingredient name">
                      </div>
                      <div class="col-6 text-start">
                        <label :for="'floatingPassword' + recipe.id" class="me-5 fs-6">Quantity</label>
                        <input v-model="editable.quantity" type="password" class="form-control"
                          :id="'floatingPassword' + recipe.id" placeholder="ex. 1 1/2 cups">
                      </div>
                      <div class="col-12">
                        <button class="btn btn-outline-primary mb-2">Add Ingredient</button>
                      </div>
                    </form>
                  </template>
                </InstructionComponent>
              </div>
            </section>
          </div>
        </section>
      </div>
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
import InstructionComponent from "./InstructionComponent.vue";
export default {
  setup() {
    onMounted(() => {
      getRecipes()
    })
    watch(() => AppState.account.id, () => {
      getMyFavorites()
    })
    let active = ref({});
    let isActive = ref(false);
    let editable = ref({});
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
      editable,
      active,
      recipes: computed(() => AppState.recipes),
      favorites: computed(() => AppState.favorites),
      account: computed(() => AppState.account),
      recipeIdString: computed(() => active.value.id.toString()),
      activeIngredients: computed(() => AppState.activeIngredients),

      async createIngredient(recipeId) {
        try {

        } catch (error) {
          Pop.error(error)
        }
      },

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

      async getIngredientsByRecipeId(recipeId) {
        try {
          await recipesService.getIngredientsByRecipeId(recipeId)
        } catch (error) {
          Pop.error(error)
        }
      },


      isFavorite(recipeId) {
        return this.favorites.some(favorite => favorite.recipeId == recipeId)
      },

      setActive(recipeId) {
        this.getIngredientsByRecipeId(recipeId)
        this.active.value = this.recipes.find(recipe => recipe.id == recipeId)
      },

      getModalId(recipeId) {
        return `#modal_${recipeId}`
      }
    }
  },
  components: { ModalComponent, InstructionComponent }
};
</script>


<style lang="scss" scoped>
.recipe-card-size {
  min-height: 25dvh;
}

.blur-bg {
  background: #8c8b8a4e;
  backdrop-filter: blur(5px);
}
</style>