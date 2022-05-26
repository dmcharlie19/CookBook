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
    private readonly ITagRepository _tagRepository;
    private readonly ITagRecipeRepository _tagRecipeRepository;

    public RecipesController( IUnitOfWork unitOfWork,
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

    [HttpGet, Route( "" )]
    public IReadOnlyList<RecipeDto>? GetAll()
    {
      return _recipeQuery.GetAll();
    }

    [HttpPost, /*Authorize,*/ Route( "AddRecipe" )]
    public IActionResult AddRecipe( [FromBody] AddRecipeRequestDto addRecipeRequest )
    {
      Console.WriteLine( addRecipeRequest.Title );

      if ( addRecipeRequest is null ||
        addRecipeRequest.CookingSteps is null ||
        addRecipeRequest.RecipeIngridients is null )
        return BadRequest();

      var recipe = new Recipe(
        addRecipeRequest.Title,
        addRecipeRequest.ShortDescription,
        addRecipeRequest.PreparingTime,
        addRecipeRequest.PersonCount,
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

      

      return Ok();
    }
  }
}

