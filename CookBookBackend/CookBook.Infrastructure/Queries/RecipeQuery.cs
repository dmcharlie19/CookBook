using System.Collections.Generic;
using System.Linq;
using CookBook.Application.Mappers;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBookBackend.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Queries
{
  public class RecipeQuery : IRecipeQuery
  {

    private readonly CookBookDbContext _dbContext;

    public RecipeQuery( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }

    public List<RecipeDto> GetAll()
    {
      var es = _dbContext.Recipes.ToList();

      return es.ConvertAll( r => RecipeMapper.Map( r ) );
    }
  }
}
