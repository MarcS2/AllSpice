import { api } from "./AxiosService"
import { logger } from "../utils/Logger"
class IngredientsService {

  async createIngredient(ingredientData) {
    const res = await api.post('api/ingredients', ingredientData)
    logger.log(res.data)
  }
}

export const ingredientsService = new IngredientsService()