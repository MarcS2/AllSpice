


namespace AllSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repository;

  public FavoritesService(FavoritesRepository repository)
  {
    _repository = repository;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    Favorite favorite = _repository.CreateFavorite(favoriteData);
    return favorite;
  }

  internal List<FavoriteRecipe> GetFavoritesByAccountId(string userId)
  {
    List<FavoriteRecipe> favorites = _repository.GetFavoritesByAccountId(userId);
    return favorites;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repository.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception($"Invalid Id:{favoriteId}");
    }
    return favorite;
  }

  internal string DestroyFavorite(int favoriteId, string userId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != userId)
    {
      throw new Exception("This is not your favorite");
    }
    _repository.DestroyFavorite(favoriteId);
    return "No more favorite";
  }
}