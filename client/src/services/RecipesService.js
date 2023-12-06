import { AppState } from "../AppState";
import { Ingredient } from "../models/Ingredient";
import { Recipe } from "../models/Recipe";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService"

class RecipesService {

  async getRecipes() {
    const res = await api.get('api/recipes');
    AppState.recipes = res.data.map(recipe => new Recipe(recipe))
    logger.log('[RecipesService] getRecipes() AppState recipes', AppState.recipes)
  }

  async getIngredientsByRecipeId(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    AppState.activeIngredients = []
    AppState.activeIngredients = res.data.map(ingredient => new Ingredient(ingredient))
    logger.log('[RecipesService] getIngredientsByRecipeId() AppState.activeIngredients', AppState.activeIngredients)
  }

  async getRecipeById(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}`)
    return res.data
  }

  getMyRecipes(creatorId) {
    const recipes = AppState.recipes.filter(recipe => recipe.creatorId == creatorId)
    AppState.recipes = recipes
  }

  async setActiveRecipe(recipeId) {
    const recipe = await this.getRecipeById(recipeId)
    AppState.activeRecipe = new Recipe(recipe)
    logger.log('[RecipesService] setActiveRecipe() AppState.activeRecipe', AppState.activeRecipe)
  }
}


export const recipesService = new RecipesService()