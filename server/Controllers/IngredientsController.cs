namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class IngredientsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly IngredientsService _ingredientsService;
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}