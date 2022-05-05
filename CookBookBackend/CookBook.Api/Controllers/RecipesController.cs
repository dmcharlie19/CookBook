using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CookBookBackend.Api.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]
  public class RecipesController : ControllerBase
  {
    private readonly IRecipeQuery _recipeQuery;

    public RecipesController( IRecipeQuery recipeQuery )
    {
      _recipeQuery = recipeQuery;
    }

    [HttpGet( "" )]
    public IReadOnlyList<RecipeDto>? GetAll()
    {
      return _recipeQuery.GetAll();
    }
  }
}

