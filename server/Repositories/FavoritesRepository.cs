



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


  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = @"SELECT * FROM favorites WHERE id = @favoriteId;";

    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return favorite;
  }

  internal List<FavoriteRecipe> GetFavoritesByAccountId(string userId)
  {
    string sql = @"
    SELECT 
    fav.*,
    rec.*,
    acc.*
    FROM favorites fav
    JOIN recipes rec ON fav.recipeId = rec.id
    JOIN accounts acc ON acc.id = rec.creatorId
    WHERE fav.accountId = @userId;";

    List<FavoriteRecipe> favorites = _db.Query<Favorite, FavoriteRecipe, Account, FavoriteRecipe>(sql, (favorite, favoriteRecipe, account) =>
    {
      favoriteRecipe.FavoriteId = favorite.Id;
      favoriteRecipe.AccountId = favorite.AccountId;
      favoriteRecipe.Creator = account;
      return favoriteRecipe;
    }, new { userId }).ToList();
    return favorites;
  }

  internal void DestroyFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1 ;";
    _db.Execute(sql, new { favoriteId });
  }
}