using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Repositories
{
  public class RecipeRepository : IRecipeRepository
  {
    private readonly CookBookDbContext _dbContext;

    public RecipeRepository( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }

    public void Add( Recipe recipe )
    {
      _dbContext.Recipes.Add( recipe );
    }
  }
}
