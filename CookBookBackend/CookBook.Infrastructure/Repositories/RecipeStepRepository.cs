using System.Collections.Generic;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Repositories
{
  public class RecipeStepRepository : IRecipeStepRepository
  {
    private readonly CookBookDbContext _dbContext;

    public RecipeStepRepository( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }

    public void Add( RecipeStep recipeStep )
    {
      _dbContext.RecipeSteps.Add( recipeStep );
    }

    public void AddRange( List<RecipeStep> recipeSteps )
    {
      _dbContext.RecipeSteps.AddRange( recipeSteps );
    }
  }
}
