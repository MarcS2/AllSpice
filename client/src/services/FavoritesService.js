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
}

export const favoritesService = new FavoritesService()