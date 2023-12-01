namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class FavoritesController
{
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoritesService;

  public FavoritesController(Auth0Provider auth0Provider, FavoritesService favoritesService)
  {
    _auth0Provider = auth0Provider;
    _favoritesService = favoritesService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Favorite>> 
  {
    
  }
}