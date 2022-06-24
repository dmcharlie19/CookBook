using System.Collections.Generic;
using System.Linq;
using CookBook.Application.Dto;
using CookBook.Application.Mappers;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Core.Domain;
using CookBook.Core.Exceptions;
using CookBook.Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CookBook.Infrastructure.Queries
{
    public class RecipeQuery : IRecipeQuery
    {

        private readonly CookBookDbContext _dbContext;
        private readonly int _pageSize = 3;   // количество элементов на странице

        public RecipeQuery( CookBookDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<RecipeShortDto> GetAll( int page )
        {
            var recipeQuery = _dbContext.Recipes.Include( r => r.User );
            var items = recipeQuery.Skip( ( page - 1 ) * _pageSize )
                .Take( _pageSize )
                .ToList();

            return items.ConvertAll( r =>
              {
                  var recipeDto = r.Map();
                  recipeDto.AuthorId = r.User.Id;
                  recipeDto.AuthorName = r.User.Name;
                  recipeDto.Tags = GetTags( r.Id );
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

        public IReadOnlyList<RecipeShortDto> GetByUserId( int userId )
        {
            var user = _dbContext.Users.FirstOrDefault( u => u.Id == userId );
            if ( user == null )
                throw new InvalidClientParameterException( "пользователь не найден" );

            return _dbContext.Recipes.
                Where( r => r.UserId == userId )
                .Include( r => r.User )
                .ToList().
                ConvertAll( r =>
                    {
                        var recipeDto = r.Map();
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

        public IReadOnlyList<RecipeShortDto> SearchRecipe( string searchRequest )
        {
            int[] tagsQuerry = _dbContext.Tags.Where( t => t.Name == searchRequest )
                    .Join( _dbContext.TagRecipes, tag => tag.Id, tagRecipe => tagRecipe.TagId, ( tag, tagRecipe ) => tagRecipe.RecipeId )
                    .ToArray();

            IIncludableQueryable<Recipe, User> recipeQuery = _dbContext.Recipes
                .Where( recipe => ( recipe.Title.Contains( searchRequest ) || tagsQuerry.Contains( recipe.Id ) ) )
                .Include( r => r.User );

            List<Recipe> items = recipeQuery
                .Take( _pageSize )
                .ToList();

            return items.ConvertAll( r =>
            {
                var recipeDto = r.Map();
                recipeDto.AuthorId = r.User.Id;
                recipeDto.AuthorName = r.User.Name;
                recipeDto.Tags = GetTags( r.Id );
                return recipeDto;
            }
             );
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
