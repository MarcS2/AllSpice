

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

  // internal List<Favorite> GetFavoritesByAccountId(string userId)
  // {
  //   List<Favorite> favorites = _repository.GetFavoritesByAccountId(userId);
  //   if (favorites == null)
  //   {
  //     throw new Exception($"Invalid Id:{userId}");
  //   }
  //   return favorites;
  // }
}