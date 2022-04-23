using System.Collections.Generic;
using CookBookBackend.Dto;
using Microsoft.AspNetCore.Mvc;
using CookBookBackend.Services;
using CookBookBackend.Domain;

namespace CookBookBackend.Controllers
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
    public List<RecipeDto>? GetAll()
    {
      try
      {
        return _recipeService.GetRecipes().Select( x => new RecipeDto
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
    public RecipeDto? GetById( [FromRoute] int recipeId )
    {
      try
      {
        var recipe = _recipeService.GetRecipe( recipeId );
        return recipe is null ? null : new RecipeDto
        {
          Id = recipe.Id,
          Title = recipe.Title,
          ShortDescription = recipe.ShortDescription,
          PreparingTime = recipe.PreparingTime
        };
      }
      catch ( Exception ex )
      {
        Console.WriteLine( ex.Message );
        return null;
      }
    }

    [HttpPost( "create" )]
    public IActionResult Create( [FromBody] RecipeDto recipe )
    {
      if ( recipe.Title == null )
        return BadRequest();

      try
      {
        int id = _recipeService.CreateRecipe( new Recipe
        {
          Title = recipe.Title,
          ShortDescription = recipe.ShortDescription,
          PreparingTime = recipe.PreparingTime
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
    public IActionResult EditRecipe( [FromBody] RecipeDto recipe )
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
