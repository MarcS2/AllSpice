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
    AppState.activeIngredients = res.data.map(ingredient => new Ingredient(ingredient))
    logger.log('[RecipesService] getIngredientsByRecipeId() AppState.activeIngredients', AppState.activeIngredients)
  }


}


export const recipesService = new RecipesService()