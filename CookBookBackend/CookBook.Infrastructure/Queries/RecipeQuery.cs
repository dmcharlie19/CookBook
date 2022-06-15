using System.Collections.Generic;
using System.Linq;
using CookBook.Api.Dto;
using CookBook.Application.Mappers;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Core.Exceptions;
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
            var recipeQuery = _dbContext.Recipes.
                Join( _dbContext.Users, recipe => recipe.UserId, user => user.Id, ( recipe, user ) =>
                new
                {
                    Recipe = recipe,
                    User = user
                } )
                .ToList();

            return recipeQuery.ConvertAll( rq =>
              {
                  var recipeDto = rq.Recipe.Map();
                  recipeDto.AuthorId = rq.User.Id;
                  recipeDto.AuthorName = rq.User.Name;
                  recipeDto.Tags = GetTags( rq.Recipe.Id );
                  return recipeDto;
              }
             );
        }

        public RecipeFullDto GetRecipeDetail( int id )
        {
            RecipeFullDto recipeFull = new();
            RecipeShortDto recipeShort = new();

            recipeShort = _dbContext.Recipes.FirstOrDefault( recipe => recipe.Id == id ).Map();
            recipeShort.AuthorName = _dbContext.Users.FirstOrDefault( user => user.Id == recipeShort.AuthorId ).Name;
            recipeShort.Tags = GetTags( id );

            recipeFull.RecipeShortInfo = recipeShort;
            recipeFull.CookingSteps = _dbContext.RecipeSteps.Where( step => step.RecipeId == id ).Select( step => step.Content ).ToArray();
            recipeFull.RecipeIngridients = _dbContext.RecipeIngredients.Where( ingr => ingr.RecipeId == id ).Select( ingr => new RecipeIngridientDto
            {
                Title = ingr.Title,
                Ingredients = ingr.GetIngredients()
            } ).ToArray();

            return recipeFull;
        }

        public IReadOnlyList<RecipeShortDto> GetRecipesByUserId( int userId )
        {
            var user = _dbContext.Users.FirstOrDefault( u => u.Id == userId );
            if ( user == null )
                throw new InvalidClientParameterException( "пользователь не найден" );

            return _dbContext.Recipes.
                Where( r => r.UserId == userId )
                .ToList().
                ConvertAll( r =>
                    {
                        var recipeDto = r.Map();
                        recipeDto.AuthorId = user.Id;
                        recipeDto.AuthorName = user.Name;
                        recipeDto.Tags = GetTags( r.Id );
                        return recipeDto;
                    }
                 );
        }

        public string GetRecipeImagePath( int recipeId )
        {
            var recipe = _dbContext.Recipes.FirstOrDefault( r => r.Id == recipeId );

            return recipe.ImagePath;
        }

        private string[] GetTags( int recipeId )
        {
            return _dbContext.TagRecipes
                .Where( tagRecipe => tagRecipe.RecipeId == recipeId )
                .Join( _dbContext.Tags, tagRecipe => tagRecipe.TagId, tag => tag.Id, ( tagRecipe, tag ) => tag.Name )
                .ToArray();
        }

    }
}
