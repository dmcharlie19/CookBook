using System.Collections.Generic;
using CookBook.Application.Dto;
using CookBook.Core.Domain;

namespace CookBook.Application.Services
{
    public interface IRecipeService
    {
        void AddRecipe( int userId, AddRecipeRequestDto addRecipeRequest, List<Tag> tags, string imgPath );
        void DeleteRecipe( int userId, int recipeId );
    }
}