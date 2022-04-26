using CookBookBackend.Api.Command;
using CookBookBackend.Application.AppService;
using CookBookBackend.Application.AppServices.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CookBookBackend.Api.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]
  public class RecipeController : ControllerBase
  {
    private readonly IRecipeService _recipeService;

    public RecipeController( IRecipeService recipeService )
    {
      _recipeService = recipeService;
    }

    [HttpGet( "get-all" )]
    public List<RecipeCommand>? GetAll()
    {
      try
      {
        return _recipeService.GetRecipes().Select( x => new RecipeCommand
        {
          Id = x.Id,
          Title = x.Title,
          ShortDescription = x.ShortDescription,
          PreparingTime = x.PreparingTime
        } ).ToList();
      }
      catch ( Exception ex )
      {
        Console.WriteLine( ex.Message );
        return null;
      }
    }

    [HttpGet( "{recipeId}" )]
    public RecipeCommand? GetById( [FromRoute] int recipeId )
    {
      try
      {
        var recieDto = _recipeService.GetRecipe( recipeId );
        return recieDto is null ? null : new RecipeCommand
        {
          Id = recieDto.Id,
          Title = recieDto.Title,
          ShortDescription = recieDto.ShortDescription,
          PreparingTime = recieDto.PreparingTime
        };
      }
      catch ( Exception ex )
      {
        Console.WriteLine( ex.Message );
        return null;
      }
    }

    [HttpPost( "create" )]
    public IActionResult Create( [FromBody] RecipeCommand recipeCommand )
    {
      if ( recipeCommand.Title == null )
        return BadRequest();

      try
      {
        int id = _recipeService.CreateRecipe( new RecipeDto
        {
          Title = recipeCommand.Title,
          ShortDescription = recipeCommand.ShortDescription,
          PreparingTime = recipeCommand.PreparingTime
        } );
        return Ok( id );
      }
      catch ( Exception ex )
      {
        Console.WriteLine( ex.Message );
        return Problem();
      }
    }

    [HttpPut( "/edit" )]
    public IActionResult EditRecipe( [FromBody] RecipeCommand recipe )
    {
      //#Todo реализовать..
      return Problem();
    }

    [HttpDelete( "{recipeId}/delete" )]
    public IActionResult Delete( [FromRoute] int recipeId )
    {
      try
      {
        _recipeService.Delete( recipeId );
        return Ok();
      }
      catch ( Exception ex )
      {
        Console.WriteLine( ex.Message );
        return BadRequest();
      }
    }
  }
}
