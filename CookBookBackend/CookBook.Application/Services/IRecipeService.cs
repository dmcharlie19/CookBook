using System.Collections.Generic;
using CookBook.Application.Dto;
using CookBook.Core.Domain;

namespace CookBook.Application.Services
{
    public interface IRecipeService
    {
        void AddFavorite( int userId, int recipeId );
        void AddLike( int userId, int recipeId );
        void AddRecipe( int userId, AddRecipeRequestDto addRecipeRequest, List<Tag> tags, string imgPath );
        void DeleteRecipe( int userId, int recipeId );
    }
}