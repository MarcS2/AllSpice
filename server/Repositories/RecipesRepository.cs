





namespace AllSpice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO 
    recipes(title, instructions, img, category, creatorId) 
    VALUES(@Title, @Instructions, @Img, @Category, @creatorId);
    
    
    SELECT 
    rec.*,
    acc.*
    FROM recipes rec
    JOIN accounts acc ON rec.creatorId = acc.id
    WHERE rec.id = LAST_INSERT_ID();";

    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, recipeData).FirstOrDefault();
    return recipe;

  }

  internal List<Recipe> GetRecipes()
  {
    string sql = @"
    SELECT 
    rec.*,
    acc.*
    FROM recipes rec
    JOIN accounts acc ON rec.creatorId = acc.id
    ;";
    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }).ToList();
    return recipes;
  }
  internal List<Recipe> GetRecipesByQuery(string category)
  {

    category = $"%{category}%";
    string sql = @"
      SELECT
      rec.*,
      acc.*
      FROM recipes rec
      JOIN accounts acc ON rec.creatorId = acc.id
      WHERE rec.category LIKE @category;";

    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { category }).ToList();

    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
      SELECT 
      rec.*,
      acc.*
      FROM recipes rec
      JOIN accounts acc ON rec.creatorId = acc.id
      WHERE rec.id = @recipeId;";

    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { recipeId }).FirstOrDefault();
    return recipe;
  }

  internal void UpdateRecipe(Recipe originalRecipe)
  {
    string sql = @"
      UPDATE recipes
      SET 
      title = @Title,
      instructions = @Instructions,
      img = @Img,
      category = @Category
      WHERE id = @Id;";
    _db.Execute(sql, originalRecipe);


  }

  internal string DeleteRecipe(int recipeId)
  {
    string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";

    _db.Execute(sql, new { recipeId });
    return "Your recipe has been deleted.";
  }

}