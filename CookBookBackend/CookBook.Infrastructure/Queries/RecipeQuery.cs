using System.Collections.Generic;
using System.Linq;
using CookBook.Application.Dto;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Application.Utils;
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
        private readonly int _userId;
        private readonly int _pageSize = 3;   // количество элементов на странице

        public RecipeQuery( CookBookDbContext dbContext, IUserIdQualifier userIdQualifier )
        {
            _dbContext = dbContext;
            _userId = userIdQualifier.GetUserId();
        }

        public IReadOnlyList<RecipeShortDto> GetRecipesPage( int page )
        {
            var recipeQuery = _dbContext.Recipes.Skip( ( page - 1 ) * _pageSize )
                .Take( _pageSize );

            var items = recipeQuery
                .Include( r => r.User )
                .Include( r => r.UserLikes )
                .Include( r => r.UserFavorites )
                .ToList()
                .ConvertAll( r => Map( r ) );

            return items;
        }

        public RecipeFullDto GetRecipeDetail( int id )
        {
            RecipeFullDto recipeFull = new();
            recipeFull.RecipeShortInfo = GetRecipeShort( id );

            recipeFull.CookingSteps = _dbContext.RecipeSteps.Where( step => step.RecipeId == id ).Select( step => step.Content ).ToArray();
            recipeFull.RecipeIngridients = _dbContext.RecipeIngredients.Where( ingr => ingr.RecipeId == id ).Select( ingr => new RecipeIngridientDto
            {
                Title = ingr.Title,
                Ingredients = ingr.GetIngredients()
            } ).ToArray();

            return recipeFull;
        }

        public RecipeShortDto GetRecipeShort( int id )
        {
            var recipe = _dbContext.Recipes
                .Include( r => r.User )
                .Include( r => r.UserLikes )
                .Include( r => r.UserFavorites )
                .FirstOrDefault( r => r.Id == id );

            return Map( recipe );
        }

        public IReadOnlyList<RecipeShortDto> GetByUserId( int userId )
        {
            var user = _dbContext.Users.FirstOrDefault( u => u.Id == userId );
            if ( user == null )
                throw new InvalidClientParameterException( "пользователь не найден" );

            return _dbContext.Recipes
                .Where( r => r.UserId == userId )
                .Include( r => r.User )
                .Include( r => r.UserLikes )
                .Include( r => r.UserFavorites )
                .ToList()
                .ConvertAll( r => Map( r ) );
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

            List<RecipeShortDto> items = recipeQuery
                .Include( r => r.UserLikes )
                .Include( r => r.UserFavorites )
                .Take( _pageSize )
                .ToList()
                .ConvertAll( r => Map( r ) );

            return items;
        }
        private string[] GetTags( int recipeId )
        {
            return _dbContext.TagRecipes
                .Where( tagRecipe => tagRecipe.RecipeId == recipeId )
                .Join( _dbContext.Tags, tagRecipe => tagRecipe.TagId, tag => tag.Id, ( tagRecipe, tag ) => tag.Name )
                .ToArray();
        }

        private bool IsUserLikeRecipe( int userId, int recipeId )
        {
            var userLike = _dbContext.UserLikes
                .FirstOrDefault( ul => ul.UserId == userId && ul.RecipeId == recipeId );

            return userLike != null;
        }

        private bool IsUserFavoriteRecipe( int userId, int recipeId )
        {
            var userFavorite = _dbContext.UserFavorites
                .FirstOrDefault( ul => ul.UserId == userId && ul.RecipeId == recipeId );

            return userFavorite != null;
        }

        private RecipeShortDto Map( Recipe recipe )
        {
            return new RecipeShortDto()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                ShortDescription = recipe.ShortDescription,
                PreparingTime = recipe.PreparingTime,
                PersonCount = recipe.PersonCount,
                AuthorId = recipe.UserId,
                AuthorName = recipe.User.Name,
                LikesCount = recipe.UserLikes.Count,
                FavoritesCount = recipe.UserFavorites.Count,

                Tags = GetTags( recipe.Id ),
                IsUserLikeRecipe = IsUserLikeRecipe( _userId, recipe.Id ),
                IsUserFavoriteRecipe = IsUserFavoriteRecipe( _userId, recipe.Id ),
            };
        }
    }
}
