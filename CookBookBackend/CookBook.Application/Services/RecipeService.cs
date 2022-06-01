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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeQuery _recipeQuery;
        private readonly IRecipeRepository _recipeRepository;

        private readonly ITagRecipeRepository _tagRecipeRepository;

        public RecipeService(
          IUnitOfWork unitOfWork,
          IRecipeQuery recipeQuery,
          IRecipeRepository recipeRepository,
          ITagRepository tagRepository,
          ITagRecipeRepository tagRecipeRepository )
        {
            _unitOfWork = unitOfWork;
            _recipeQuery = recipeQuery;
            _recipeRepository = recipeRepository;

            _tagRecipeRepository = tagRecipeRepository;
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
              .Select( recipeStep => new RecipeStep( recipeStep, recipe.Id ) )
              .ToList() );

            recipe.AddRecipeIngredients( addRecipeRequest.RecipeIngridients
              .Select( ri => new RecipeIngredient( ri.IngridientTitle, ri.IngridientBody, recipe.Id ) )
              .ToList() );

            //recipe.AddTags( tags );

            // Валидация модели
            var results = new List<ValidationResult>();
            var context = new ValidationContext( recipe );
            if ( !Validator.TryValidateObject( recipe, context, results, true ) )
                throw new InvalidClientParameterException( results[ 0 ].ErrorMessage );

            _recipeRepository.Add( recipe );
        }
    }
}
