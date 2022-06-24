using System.Collections.Generic;
using CookBook.Application.Queries.Dto;

namespace CookBook.Application.Queries
{
    public interface IRecipeQuery
    {
        IReadOnlyList<RecipeShortDto> GetAll( int page );
        RecipeFullDto GetRecipeDetail( int id );
        IReadOnlyList<RecipeShortDto> GetByUserId( int userId );
        IReadOnlyList<RecipeShortDto> SearchRecipe( string searchRequest );
        string GetRecipeImagePath( int recipeId );
        RecipeShortDto GetRecipeShort( int id );
    }
}
