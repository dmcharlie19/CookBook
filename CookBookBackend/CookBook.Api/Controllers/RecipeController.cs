using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CookBookBackend.Api.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]
  public class RecipeController : ControllerBase
  {
    private readonly IRecipeQuery _recipeQuery;

    public RecipeController( IRecipeQuery recipeQuery )
    {
      _recipeQuery = recipeQuery;
    }

    [HttpGet( "" )]
    public List<RecipeDto>? GetAll()
    {
      return _recipeQuery.GetAll();
    }
  }
}

