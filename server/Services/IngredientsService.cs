

namespace AllSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repository;

  public IngredientsService(IngredientsRepository repository)
  {
    _repository = repository;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _repository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repository.GetIngredientsByRecipeId(recipeId);

    if (ingredients == null)
    {
      throw new Exception($"Invalid Id:{recipeId}");
    }

    return ingredients;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repository.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception($"Invalid Id:{ingredientId}");
    }
    return ingredient;
  }

  internal string DeleteIngredient(int ingredientId)
  {
    Ingredient foundIngredient = GetIngredientById(ingredientId);
    string message = _repository.DeleteIngredient(ingredientId);
    return (message);
  }
}
