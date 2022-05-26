using System.Collections.Generic;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Repositories
{
  public class TagRecipeRepository : ITagRecipeRepository
  {
    private readonly CookBookDbContext _dbContext;

    public TagRecipeRepository( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }

    public void Add( TagRecipe tr )
    {
      _dbContext.TagRecipes.Add( tr );
    }

    public void AddRange( List<TagRecipe> tr )
    {
      _dbContext.TagRecipes.AddRange( tr );
    }
  }
}
