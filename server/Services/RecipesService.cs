


namespace AllSpice.Services;

public class RecipesService
{

  private readonly RecipesRepository _repository;

  public RecipesService(RecipesRepository repository)
  {
    _repository = repository;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repository.CreateRecipe(recipeData);
    return recipe;
  }

  internal List<Recipe> GetRecipes()
  {
    List<Recipe> recipes = _repository.GetRecipes();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repository.GetRecipeById(recipeId);

    if (recipe == null)
    {
      throw new Exception($"Invalid Id:{recipeId}");
    }

    return recipe;
  }
}