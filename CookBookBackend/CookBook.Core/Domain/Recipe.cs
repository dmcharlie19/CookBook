using System.Collections.Generic;

namespace CookBook.Core.Domain
{
  public class Recipe
  {
    public int Id { get; protected set; }
    public string? Title { get; protected set; }
    public string? ShortDescription { get; protected set; }
    public int PreparingTime { get; protected set; }
    public int PersonCount { get; set; }
    public int LikesCount { get; protected set; }
    public int FavoritesCount { get; protected set; }

    public List<TagRecipe> Tags { get; set; }
    public List<RecipeStep> RecipeSteps { get; set; }
    public List<RecipeIngredient> RecipeIngredients { get; set; }

    public Recipe(
        string title,
        string shortDescription,
        int preparingTime,
        int personCount,
        int likesCount,
        int favoritesCount )
    {
      Title = title;
      ShortDescription = shortDescription;
      PreparingTime = preparingTime;
      PersonCount = personCount;
      LikesCount = likesCount;
      FavoritesCount = favoritesCount;
    }
  }
}
