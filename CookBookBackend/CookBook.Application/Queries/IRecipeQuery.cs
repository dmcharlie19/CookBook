using System.Collections.Generic;
using CookBook.Application.Queries.Dto;

namespace CookBook.Application.Queries
{
    public interface IRecipeQuery
    {
        IReadOnlyList<RecipeShortDto> GetAll();
        RecipeFullDto GetRecipeDetail( int id );
        IReadOnlyList<RecipeShortDto> GetRecipesByUserId( int userId );
    }
}
