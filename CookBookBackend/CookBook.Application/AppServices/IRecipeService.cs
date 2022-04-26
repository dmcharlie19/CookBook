using System.Collections.Generic;
using CookBookBackend.Application.AppServices.Dto;

namespace CookBookBackend.Application.AppService
{
  public interface IRecipeService
  {
    List<RecipeDto> GetRecipes();
    RecipeDto? GetRecipe( int recipeId );
    void EditRecipe( RecipeDto recipeDto );
    int CreateRecipe( RecipeDto recipeDto );
    void Delete( int recipeId );
  }
}
