using System.Collections.Generic;
using System.Linq;
using CookBook.Application.Mappers;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Queries
{
  public class RecipeQuery : IRecipeQuery
  {

    private readonly CookBookDbContext _dbContext;

    public RecipeQuery( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }

    public IReadOnlyList<RecipeDto> GetAll()
    {
      var es = _dbContext.Recipes.ToList();

      return es.ConvertAll( r => r.Map() );
    }
  }
}
