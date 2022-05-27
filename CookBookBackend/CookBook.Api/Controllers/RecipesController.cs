using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using CookBook.Application.Repositories;
using CookBook.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace CookBook.Api.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]

  public class RecipesController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecipeQuery _recipeQuery;
    private readonly IRecipeService _recipeService;

    public RecipesController( IUnitOfWork unitOfWork,
      IRecipeQuery recipeQuery,
      IRecipeService recipeService )
    {
      _unitOfWork = unitOfWork;
      _recipeQuery = recipeQuery;
      _recipeService = recipeService;
    }

    [HttpGet, Route( "" )]
    public IReadOnlyList<RecipeDto>? GetAll()
    {
      return _recipeQuery.GetAll();
    }

    [HttpPost, Authorize, Route( "AddRecipe" )]
    public void AddRecipe( [FromBody] AddRecipeRequestDto addRecipeRequest )
    {
      _recipeService.AddRecipe( addRecipeRequest );
      _unitOfWork.Commit();
    }
  }
}

