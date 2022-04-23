using CookBookBackend.Domain;
using CookBookBackend.Repositories;
using CookBookBackend.Storage;

namespace CookBookBackend.Services
{
  public class RecipeService : IRecipeService
  {
    IRecipeRepository _recipeRepository;
    IUnitOfWork _unitOfWork;
    public RecipeService( IRecipeRepository recipeRepository, IUnitOfWork unitOfWork )
    {
      _recipeRepository = recipeRepository;
      _unitOfWork = unitOfWork;
    }

    public void EditRecipe( Recipe recipe )
    {
      //#Todo реализовать..
      throw new NotImplementedException();
    }

    public int CreateRecipe( Recipe recipe )
    {
      _recipeRepository.Create( recipe );
      _unitOfWork.Commit();

      return recipe.Id;
    }

    public void Delete( int recipeId )
    {
      _recipeRepository.Delete( recipeId );
      _unitOfWork.Commit();
    }

    public Recipe? GetRecipe( int recipeId )
    {
      return _recipeRepository.Get( recipeId );
    }

    public List<Recipe> GetRecipes()
    {
      return _recipeRepository.GetRecipes();
    }
  }
}
