using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Infrastructure.Repositories;

namespace CookBook.Api.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]

  public class RecipesController : ControllerBase
  {
    private IUnitOfWork _unitOfWork;
    private readonly IRecipeQuery _recipeQuery;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IRecipeStepRepository _recipeStepRepository;
    private readonly IRecipeIngredientRepository _recipeIngredientRepository;

    public RecipesController( IUnitOfWork unitOfWork,
      IRecipeQuery recipeQuery, IRecipeRepository recipeRepository, IRecipeStepRepository recipeStepRepository, IRecipeIngredientRepository recipeIngredientRepository )
    {
      _unitOfWork = unitOfWork;
      _recipeQuery = recipeQuery;
      _recipeRepository = recipeRepository;
      _recipeStepRepository = recipeStepRepository;
      _recipeIngredientRepository = recipeIngredientRepository;
    }

    [HttpGet, Route( "" )]
    public IReadOnlyList<RecipeDto>? GetAll()
    {
      return _recipeQuery.GetAll();
    }

    [HttpPost, /*Authorize,*/ Route( "AddRecipe" )]
    public IActionResult AddRecipe( [FromBody] AddRecipeRequestDto addRecipeRequest )
    {
      Console.WriteLine( addRecipeRequest.Title );

      var recipe = new Recipe(
        addRecipeRequest.Title,
        addRecipeRequest.ShortDescription,
        addRecipeRequest.PreparingTime,
        addRecipeRequest.PersonCount,
        tags: "",
        likesCount: 0,
        favoritesCount: 0 );

      _recipeRepository.Add( recipe );
      _unitOfWork.Commit();

      _recipeStepRepository.AddRange( addRecipeRequest.CookingSteps.Select(
        recipeStep => new RecipeStep( recipeStep, recipe.Id ) ).
        ToList() );

      _recipeIngredientRepository.AddRange( addRecipeRequest.RecipeIngridients.Select(
        ri => new RecipeIngredient( ri.IngridientTitle, ri.IngridientBody, recipe.Id ) ).
        ToList() );

      _unitOfWork.Commit();

      return Ok();
    }
  }
}

