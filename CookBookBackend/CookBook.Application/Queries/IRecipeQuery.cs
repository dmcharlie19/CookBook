using System.Collections.Generic;
using CookBook.Application.Queries.Dto;

namespace CookBook.Application.Queries
{
    public interface IRecipeQuery
    {
        IReadOnlyList<RecipeShortDto> GetAll();
        RecipeFullDto GetRecipeDetail( int id );
        string GetRecipeImagePath( int recipeId );
        IReadOnlyList<RecipeShortDto> GetRecipesByUserId( int userId );
    }
}
