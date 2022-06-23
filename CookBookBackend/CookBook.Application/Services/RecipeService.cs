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
        public void AddRecipe( int userId, AddRecipeRequestDto addRecipeRequest, List<Tag> tags, string imgPath )
        {
            if ( addRecipeRequest is null )
                throw new InvalidClientParameterException( "запрос не должен быть Null" );

            var recipe = new Recipe(
                addRecipeRequest.Title,
                addRecipeRequest.ShortDescription,
                addRecipeRequest.PreparingTime,
                addRecipeRequest.PersonCount,
                userId,
                imgPath );

            recipe.AddRecipeSteps( addRecipeRequest.CookingSteps
              .Select( recipeStep => new RecipeStep( recipeStep ) )
              .ToList() );

            recipe.AddRecipeIngredients( addRecipeRequest.RecipeIngridients
              .Select( ri => new RecipeIngredient( ri.Title, RecipeIngredient.Convert( ri.Ingredients ) ) )
              .ToList() );

            recipe.AddTags( tags );

            // Валидация модели
            recipe.Validate();

            _recipeRepository.Add( recipe );
        }

        public void DeleteRecipe( int userId, int recipeId )
        {
            var recipe = _recipeRepository.Get( recipeId );

            if ( recipe is null )
                throw new InvalidClientParameterException( "Рецепт не найден" );

            if ( recipe.User.Id != userId )
                throw new InvalidClientParameterException( "Удалить рецепт может только автор рецепта" );

            _recipeRepository.Delete( recipe );
        }
    }
}
