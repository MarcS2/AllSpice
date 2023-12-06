

<template>
  <section class="row justify-content-center">
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
          <div v-if="favorites == null">
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
        <div class="col-12">
          <div role="button" data-bs-toggle="modal" :data-bs-target="'#modal_One'" @click="setActive(recipe.id)">
            <span class="d-flex">
              <p class="fs-4 text-light blur-bg rounded">{{ recipe.title }}</p>
            </span>
          </div>
          <div v-if="account == recipe.creatorId" class="text-end">
            <button @click="deleteRecipe(recipe.id)" class="btn btn-outline-danger"><i
                class="mdi mdi-delete"></i></button>
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
import { favoritesService } from "../services/FavoritesService";
import Pop from "../utils/Pop";
import { Favorite } from "../models/Favorite";
export default {
  props: {
    recipe: { type: Object, required: true }
  },
  setup() {
    return {
      account: computed(() => AppState.account.id),
      favorites: computed(() => AppState.favorites),

      async createFavorite(recipeId) {
        try {
          await favoritesService.createFavorite(recipeId)
        } catch (error) {
          Pop.error(error)
        }
      },

      async deleteFavorite(recipeId) {
        try {
          const foundFavorite = this.favorites.find(favorite => favorite.id == recipeId)
          const foundFavoriteId = foundFavorite.favoriteId
          await favoritesService.deleteFavorite(foundFavoriteId)
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

      async setActiveRecipe(recipeId) {
        try {
          recipesService.setActiveRecipe(recipeId)
        } catch (error) {
          Pop.error(error)
        }
      },

      async deleteRecipe(recipeId) {
        try {
          const wantsTo = await Pop.confirm('Would you like to delete this recipe?')
          if (!wantsTo) {
            return
          }
          recipesService.deleteRecipe(recipeId)
        } catch (error) {
          Pop.error(error)
        }
      },

      isFavorite(recipeId) {
        return this.favorites.some(favorite => favorite.id == recipeId)
      },

      setActive(recipeId) {
        this.setActiveRecipe(recipeId)
        this.getIngredientsByRecipeId(recipeId)
        // this.active.value = this.recipes.find(recipe => recipe.id == recipeId)
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