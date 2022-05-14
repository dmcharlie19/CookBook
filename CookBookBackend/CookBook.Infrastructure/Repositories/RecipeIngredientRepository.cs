using System.Collections.Generic;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Repositories
{
  public class RecipeIngredientRepository : IRecipeIngredientRepository
  {
    private readonly CookBookDbContext _dbContext;

    public RecipeIngredientRepository( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }

    public void Add( RecipeIngredient recipeIngredient )
    {
      _dbContext.RecipeIngredients.Add( recipeIngredient );
    }

    public void AddRange( List<RecipeIngredient> recipeIngredients )
    {
      _dbContext.RecipeIngredients.AddRange( recipeIngredients );
    }
  }
}
