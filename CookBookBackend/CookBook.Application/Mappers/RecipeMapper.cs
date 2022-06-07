using CookBook.Application.Queries.Dto;
using CookBook.Core.Domain;

namespace CookBook.Application.Mappers
{
    public static class RecipeMapper
    {
        public static RecipeShortDto Map( this Recipe recipe )
        {
            return new RecipeShortDto()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                ShortDescription = recipe.ShortDescription,
                PreparingTime = recipe.PreparingTime,
                PersonCount = recipe.PersonCount,
                AuthorId = recipe.UserId
            };
        }
    }
}
