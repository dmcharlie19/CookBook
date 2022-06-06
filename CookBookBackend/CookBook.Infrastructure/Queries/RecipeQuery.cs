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

        public IReadOnlyList<RecipeShortDto> GetAll()
        {
            var recipeQuerry = _dbContext.Recipes.
                Join( _dbContext.Users, recipe => recipe.UserId, user => user.Id, ( recipe, user ) =>
                new
                {
                    Recipe = recipe,
                    User = user
                } )
                .ToList();

            return recipeQuerry.ConvertAll( rq =>
              {
                  var tags = _dbContext.TagRecipes
                    .Where( tagRecipe => tagRecipe.RecipeId == rq.Recipe.Id )
                    .Join( _dbContext.Tags, tagRecipe => tagRecipe.TagId, tag => tag.Id, ( tagRecipe, tag ) => tag.Name )
                    .ToArray();


                  var recipeDto = rq.Recipe.Map();
                  recipeDto.AuthorId = rq.User.Id;
                  recipeDto.AuthorName = rq.User.Name;
                  recipeDto.Tags = tags;
                  return recipeDto;
              }
             );
        }
    }
}
