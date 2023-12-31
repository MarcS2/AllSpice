<template>
  <section class="row justify-content-center">
    <div class="col-12 col-md-6 mt-3 shadow rounded-pill">
      <section class="row">
        <div class="col-4 text-center fs-5">
          <router-link :to="{ name: 'Recipe' }" class="text-dark">
            MyRecipes
          </router-link>
        </div>
        <div class="col-4 text-center fs-5">
          <router-link :to="{ name: 'HomeChild' }" class="text-dark">
            Home
          </router-link>
        </div>
        <div class="col-4 text-center fs-5 border-bottom border-primary border-3 rounded-right">
          Favorites
        </div>
      </section>
    </div>
  </section>
  <section class="row px-4 mt-4">
    <div v-for="favorite in favorites" :key="favorite.favoriteId" class="col-4 my-2">
      <RecipeCard :recipe="favorite" />
    </div>
  </section>
  <ModalComponent :modalId="'modal_One'" :modalSize="'modal-xl'">

    <template #modalTitle v-if="activeRecipe != null">
      <b>Modal One</b>
    </template>
    <template #modalBody v-if="activeRecipe != null">
      <div class="container-fluid">
        <section class="row">
          <div class="col-5">
            <section class="row justify-content-start"
              :style="{ backgroundImage: `url('${activeRecipe.img}')`, backgroundPosition: 'center', backgroundSize: 'cover', minHeight: '60dvh' }">
              <div class="col-1 text-start fs-4 mt-2">
                <div v-if="!account.id">
                </div>
                <div v-else-if="isActive">

                  <span v-if="isFavorite(activeRecipe.id)" class="blur-bg rounded p-2 px-3 " role="button"
                    @click="deleteFavorite(activeRecipe.id)">
                    <i class="mdi mdi-heart"></i>
                  </span>
                  <span v-else-if="isFavorite(activeRecipe.id) == false" class="blur-bg rounded p-2 px-3" role="button"
                    @click="createFavorite(activeRecipe.id)">
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
                  {{ activeRecipe.title }}
                </p>
                <p class="d-inline blur-bg p-1 rounded-pill">{{ activeRecipe.category }}</p>
              </div>
              <div class="col-6">
                <InstructionComponent>
                  <template #title>
                    <p class="mb-0 text-light">Instructions</p>
                  </template>
                  <template #instructions>
                    <p class="text-start">{{ activeRecipe.instructions }}</p>
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
                        {{ activeIngredient.quantity + " " + activeIngredient.name }}
                      </p>
                    </div>
                    <form v-if="activeRecipe.creatorId = account.id" @submit.prevent="createIngredient(activeRecipe.id)"
                      class="row mt-2">
                      <div class="col-6 mb-3 text-start">
                        <label :for="'floatingInput' + activeRecipe.id" class=" fs-6">Name</label>
                        <input v-model="editable.name" type="string" class="form-control "
                          :id="'floatingInput' + activeRecipe.id" placeholder="Ingredient">
                      </div>
                      <div class="col-6 text-start">
                        <label :for="'floatingPassword' + activeRecipe.id" class="me-5 fs-6">Quantity</label>
                        <input v-model="editable.quantity" type="string" class="form-control"
                          :id="'floatingPassword' + activeRecipe.id" placeholder="ex. 1 1/2 cups">
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
import {
  computed, reactive, onMounted, ref, watch
} from 'vue';
import RecipeCard from "./RecipeCard.vue";
import ModalComponent from "./ModalComponent.vue";
import InstructionComponent from "./InstructionComponent.vue";
import { logger } from "../utils/Logger";
import { useRouter } from "vue-router";
import { Recipe } from "../models/Recipe";
export default {
  setup() {
    let isActive = ref(false);
    let editable = ref({});
    let favoriteRecipes = []
    const router = useRouter()
    const account = computed(() => AppState.account)
    const favorites = computed(() => AppState.favorites)
    watch(() => AppState.favorites, () => isActive.value = true)

    return {
      account,
      isActive,
      favorites,
      activeRecipe: computed(() => AppState.activeRecipe),
      activeIngredient: computed(() => AppState.activeIngredients),
      recipes: computed(() => AppState.recipes),

      async createIngredient(recipeId) {
        try {
          editable,
            isActive,
            editable.value.recipeId = recipeId
          const ingredientData = editable.value
          await ingredientsService.createIngredient(ingredientData)
          editable.value = {}
          Modal.getOrCreateInstance(`#modal_${recipeId}`).hide()
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

      isFavorite(recipeId) {
        return this.favorites.some(favorite => favorite.id == recipeId)
      },

      setActive(recipeId) {
        this.getIngredientsByRecipeId(recipeId)
      }
    }
  },
  components: { RecipeCard, ModalComponent, InstructionComponent }
};
</script>

  

<style lang="scss" scoped>
.rounded-right {
  border-bottom-right-radius: 1rem;
}
</style>