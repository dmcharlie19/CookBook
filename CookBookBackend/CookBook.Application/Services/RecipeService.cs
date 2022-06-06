using System.Collections.Generic;
using System.Linq;
using CookBook.Api.Dto;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Core.Exceptions;

namespace CookBook.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService( IRecipeRepository recipeRepository )
        {
            _recipeRepository = recipeRepository;
        }
        public void AddRecipe( int userId, AddRecipeRequestDto addRecipeRequest, List<Tag> tags )
        {
            if ( addRecipeRequest is null )
                throw new InvalidClientParameterException( "запрос не должен быть Null" );

            var recipe = new Recipe(
                addRecipeRequest.Title,
                addRecipeRequest.ShortDescription,
                addRecipeRequest.PreparingTime,
                addRecipeRequest.PersonCount,
                userId );

            recipe.AddRecipeSteps( addRecipeRequest.CookingSteps
              .Select( recipeStep => new RecipeStep( recipeStep ) )
              .ToList() );

            recipe.AddRecipeIngredients( addRecipeRequest.RecipeIngridients
              .Select( ri => new RecipeIngredient( ri.IngridientTitle, ri.IngridientBody ) )
              .ToList() );

            recipe.AddTags( tags );

            // Валидация модели
            recipe.Validate();

            _recipeRepository.Add( recipe );
        }
    }
}
