using CookBook.Application.Queries.Dto;
using CookBookBackend.Core.Domain;

namespace CookBook.Application.Mappers
{
  public class RecipeMapper
  {
    public static RecipeDto Map( Recipe r )
    {
      return new RecipeDto()
      {
        Id = r.Id,
        Title = r.Title,
        ShortDescription = r.ShortDescription,
        PreparingTime = r.PreparingTime,
        Tags = r.Tags.Split( "_" ),
        LikesCount = r.LikesCount,
        FavoritesCount = r.FavoritesCount

      };
    }
  }
}
