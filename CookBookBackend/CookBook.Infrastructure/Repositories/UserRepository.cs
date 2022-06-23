using System.Linq;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CookBookDbContext _dbContext;

        public UserRepository( CookBookDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public void Add( User user )
        {
            _dbContext.Users.Add( user );
        }

        public User Get( string login )
        {
            return _dbContext.Users
                 .Include( u => u.UserFavorites )
                 .Include( u => u.UserLikes )
                .FirstOrDefault( user => user.Login == login );
        }

        public User Get( int id )
        {
            return _dbContext.Users
                      .Include( u => u.UserFavorites )
                      .Include( u => u.UserLikes )
                      .FirstOrDefault( user => user.Id == id );
        }
    }
}
