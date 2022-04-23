using CookBookBackend.Domain;
using CookBookBackend.Dto;

namespace CookBookBackend.Services
{
  public interface IRecipeService
  {
    List<Recipe> GetRecipes();
    Recipe? GetRecipe( int recipeId );
    void EditRecipe( Recipe recipe );
    int CreateRecipe( Recipe recipe );
    void Delete( int recipeId );
  }
}
