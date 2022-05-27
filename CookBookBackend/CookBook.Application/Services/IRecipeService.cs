using CookBook.Api.Dto;

namespace CookBook.Application.Services
{
  public interface IRecipeService
  {
    void AddRecipe( AddRecipeRequestDto addRecipeRequest );
  }
}