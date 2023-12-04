

<template>
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
          @click="getIngredientsByRecipeId(recipe.id)">
          <div>
            <span class="d-flex">
              <p class="fs-4 text-light blur-bg rounded">{{ recipe.title }}</p>
            </span>
          </div>
        </div>
      </section>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from "../models/Recipe";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";
export default {
  props: {
    recipe: { type: Recipe, required: true }
  },
  setup() {
    return {
      account: computed(() => AppState.account),

      async getIngredientsByRecipeId(recipeId) {
        try {
          debugger
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
  }
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