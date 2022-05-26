using CookBook.Application.Queries.Dto;
using CookBook.Core.Domain;

namespace CookBook.Application.Mappers
{
  public static class RecipeMapper
  {
    public static RecipeDto Map( this Recipe recipe )
    {
      return new RecipeDto()
      {
        Id = recipe.Id,
        Title = recipe.Title,
        ShortDescription = recipe.ShortDescription,
        PreparingTime = recipe.PreparingTime,
        PersonCount = recipe.PersonCount,
        LikesCount = recipe.LikesCount,
        FavoritesCount = recipe.FavoritesCount
      };
    }
  }
}
