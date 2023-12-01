
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
}
