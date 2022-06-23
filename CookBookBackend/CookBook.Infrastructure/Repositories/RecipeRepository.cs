using System.Linq;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

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

        public Recipe Get( int id )
        {
            return _dbContext.Recipes.Include( r => r.User ).FirstOrDefault( r => r.Id == id );
        }

        public void Delete( Recipe recipe )
        {
            _dbContext.Recipes.Remove( recipe );
        }
    }
}
