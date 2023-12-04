import { api } from "./AxiosService"
import { logger } from "../utils/Logger"
import { AppState } from "../AppState"
import { Favorite } from "../models/Favorite"
class FavoritesService {
  async getMyFavorites() {
    const res = await api.get('account/favorites')
    AppState.favorites = res.data.map(favorite => new Favorite(favorite))
    logger.log("[FavoritesService] getMyFavorites() AppState.favorites", AppState.favorites)
  }

  async createFavorite(recipeId) {
    await api.post('api/favorites', { recipeId })
    this.getMyFavorites(recipeId)
  }

  async deleteFavorite(favoriteId) {
    await api.delete(`api/favorites/${favoriteId}`);
    const favoriteIndex = AppState.favorites.findIndex(favorite => favorite.favoriteId == favoriteId)
    AppState.favorites.splice(favoriteIndex, 1)
  }
}

export const favoritesService = new FavoritesService()