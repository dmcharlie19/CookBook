using System.Collections.Generic;
using System.Linq;
using CookBook.Application.Mappers;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Core.Domain;
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
      var recipes = _dbContext.Recipes.ToList();

      return recipes.ConvertAll( r =>
        {
          var tags = _dbContext.TagRecipes
            .Where( tagRecipe => tagRecipe.RecipeId == r.Id )
            .Join( _dbContext.Tags, tagRecipe => tagRecipe.TagId, tag => tag.Id, ( tagRecipe, tag ) => tag.Name )
            .ToArray();

          var recipeDto = r.Map();
          recipeDto.Tags = tags;
          return recipeDto;
        }
       );
    }
  }
}
