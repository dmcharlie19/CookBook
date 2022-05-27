using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Api.Dto;
using CookBook.Application.Exceptions;
using CookBook.Application.Queries;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;

namespace CookBook.Application.Services
{
  public class RecipeService : IRecipeService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecipeQuery _recipeQuery;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IRecipeStepRepository _recipeStepRepository;
    private readonly IRecipeIngredientRepository _recipeIngredientRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ITagRecipeRepository _tagRecipeRepository;

    public RecipeService(
      IUnitOfWork unitOfWork,
      IRecipeQuery recipeQuery,
      IRecipeRepository recipeRepository,
      IRecipeStepRepository recipeStepRepository,
      IRecipeIngredientRepository recipeIngredientRepository,
      ITagRepository tagRepository,
      ITagRecipeRepository tagRecipeRepository )
    {
      _unitOfWork = unitOfWork;
      _recipeQuery = recipeQuery;
      _recipeRepository = recipeRepository;
      _recipeStepRepository = recipeStepRepository;
      _recipeIngredientRepository = recipeIngredientRepository;
      _tagRepository = tagRepository;
      _tagRecipeRepository = tagRecipeRepository;
    }
    public void AddRecipe( AddRecipeRequestDto addRecipeRequest )
    {
      if ( addRecipeRequest is null )
        throw new InvalidClientParameterException( "запрос не должен быть Null" );

      if ( addRecipeRequest.CookingSteps is null ||
           addRecipeRequest.CookingSteps.Length == 0 )
        throw new InvalidClientParameterException( "Шаги приготовления не должны быть пустыми" );

      if ( addRecipeRequest.RecipeIngridients is null ||
           addRecipeRequest.RecipeIngridients.Length == 0 )
        throw new InvalidClientParameterException( "Ингриденты рецепта не должны быть пустыми" );

      if ( addRecipeRequest.Tags is null ||
           addRecipeRequest.Tags.Length == 0 )
        throw new InvalidClientParameterException( "Тэги не должны быть пустыми" );

      var recipe = new Recipe(
        addRecipeRequest.Title,
        addRecipeRequest.ShortDescription,
        addRecipeRequest.PreparingTime,
        addRecipeRequest.PersonCount,
        likesCount: 0,
        favoritesCount: 0 );

      recipe.RecipeSteps = addRecipeRequest.CookingSteps
        .Select( recipeStep => new RecipeStep( recipeStep, recipe.Id ) )
        .ToList();

      recipe.RecipeIngredients = addRecipeRequest.RecipeIngridients
        .Select( ri => new RecipeIngredient( ri.IngridientTitle, ri.IngridientBody, recipe.Id ) )
        .ToList();

      // #TODO Валидация модели

      _recipeRepository.Add( recipe );
      _unitOfWork.Commit();

      // Работа с тегами
      if ( addRecipeRequest.Tags is not null )
      {
        foreach ( var tagName in addRecipeRequest.Tags )
        {
          Tag tag = _tagRepository.Get( tagName );
          if ( tag is null )
          {
            tag = new Tag( tagName );
            _tagRepository.Add( tag );
          }
          _unitOfWork.Commit();

          _tagRecipeRepository.Add( new TagRecipe( recipe.Id, tag.Id ) );
          _unitOfWork.Commit();
        }
      }
    }
  }
}
