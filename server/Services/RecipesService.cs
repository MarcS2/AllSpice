


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

  internal Recipe UpdateRecipe(int recipeId, Recipe recipeData)
  {
    Recipe originalRecipe = GetRecipeById(recipeId);
    originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;
    originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;
    originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;
    originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;

    _repository.UpdateRecipe(originalRecipe);
    return originalRecipe;
  }

  internal string DeleteRecipe(int recipeId, string userId)
  {
    Recipe foundRecipe = GetRecipeById(recipeId);
    if (foundRecipe.CreatorId != userId)
    {
      throw new Exception("This is not your recipe");
    }
    string message = _repository.DeleteRecipe(recipeId);
    return message;
  }
}