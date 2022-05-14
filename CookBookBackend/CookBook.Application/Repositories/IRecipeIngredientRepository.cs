using System.Collections.Generic;
using CookBook.Core.Domain;

namespace CookBook.Application.Repositories
{
  public interface IRecipeIngredientRepository
  {
    void Add( RecipeIngredient recipeIngredient );

    void AddRange( List<RecipeIngredient> recipeIngredients );
  }
}
