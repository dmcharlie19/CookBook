using System.Collections.Generic;
using CookBook.Core.Domain;

namespace CookBook.Application.Repositories
{
  public interface IRecipeStepRepository
  {
    void Add( RecipeStep recipe );

    void AddRange( List<RecipeStep> recipeSteps );
  }
}
