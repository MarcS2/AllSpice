

namespace AllSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
      INSERT INTO
      favorites(accountId, recipeId)
      VALUES(@AccountId, @RecipeId);
      
      
      SELECT 
      *
      FROM favorites
      WHERE id = LAST_INSERT_ID();";
    // JOIN recipes rec ON fav.recipeId = rec.id

    Favorite favorite = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();
    return favorite;

  }

  // internal List<Favorite> GetFavoritesByAccountId(string userId)
  // {
  //   string sql = @"
  //   SELECT 
  //   fav.*,
  //   rec.*
  //   FROM favorites fav
  //   JOIN recipes rec ON fav.recipeId = rec.id
  //   WHERE fav.accountId = @userId;";

  //   List<Favorite> favorites = _db.Query<Favorite, Recipe, Favorite>(sql, (favorite, recipe) =>
  //   {
  //     favorite.Recipe = recipe;
  //     return favorite;
  //   }, new { userId }).ToList();
  //   return favorites;
  // }
}