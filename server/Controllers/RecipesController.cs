namespace AllSpice.Controllers;

[ApiController]
[Router("api/[controller]")]

public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;

  public RecipesController(RecipesService recipesService)
  {
    _recipesService = recipesService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] RecipesController recipeData)
  {
    try
    {

    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}