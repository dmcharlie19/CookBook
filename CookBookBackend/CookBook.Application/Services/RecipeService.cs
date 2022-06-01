using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Api.Dto;
using CookBook.Application.Queries;
using CookBook.Application.Repositories;
using CookBook.Core.Exceptions;
using CookBook.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService( IRecipeRepository recipeRepository )
        {
            _recipeRepository = recipeRepository;
        }
        public void AddRecipe( AddRecipeRequestDto addRecipeRequest, List<Tag> tags )
        {
            if ( addRecipeRequest is null )
                throw new InvalidClientParameterException( "запрос не должен быть Null" );

            var recipe = new Recipe(
              addRecipeRequest.Title,
              addRecipeRequest.ShortDescription,
              addRecipeRequest.PreparingTime,
              addRecipeRequest.PersonCount );

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
