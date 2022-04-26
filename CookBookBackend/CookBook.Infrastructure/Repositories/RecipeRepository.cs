using System.Collections.Generic;
using System.Linq;
using CookBookBackend.Application.Repositories;
using CookBookBackend.Core.Domain;
using CookBookBackend.Infrastructure.Foundation;

namespace CookBookBackend.Repositories
{
  public class RecipeRepository : IRecipeRepository
  {
    private readonly CookBookDbContext _dbContext;

    public RecipeRepository( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }

    public void Create( Recipe recipe )
    {
      _dbContext.RecipeSet.Add( recipe );
    }

    public void Delete( int id )
    {
      Recipe? recipe = Get( id );
      if ( recipe == null )
      {
        throw new KeyNotFoundException();
      }

      _dbContext.RecipeSet.Remove( recipe );
    }

    public Recipe? Get( int id )
    {
      return _dbContext.RecipeSet.FirstOrDefault( x => x.Id == id );
    }

    public List<Recipe> GetRecipes()
    {
      return _dbContext.RecipeSet.ToList();
    }

    public void Update( Recipe recipe )
    {
      _dbContext.RecipeSet.Update( recipe );
    }
  }
}
