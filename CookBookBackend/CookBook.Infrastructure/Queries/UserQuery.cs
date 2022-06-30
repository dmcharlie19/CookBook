using System.Linq;
using CookBook.Application.Queries;
using CookBook.Application.Queries.Dto;
using CookBook.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Queries
{
    public class UserQuery : IUserQuery
    {
        private readonly CookBookDbContext _dbContext;

        public UserQuery( CookBookDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public UserInfoDto GetUserInfo( int userId )
        {
            var user = _dbContext.Users.FirstOrDefault( u => u.Id == userId );
            if ( user == null )
                return null;

            int recipesCount = _dbContext.Recipes.Where( r => r.UserId == userId ).Count();
            return new UserInfoDto
            {
                Id = user.Id,
                RecipesCount = recipesCount,
                UserName = user.Name,
                UserDescription = "позитивный человек, повар, любит десерты и мясо",
                RegistrationDate = System.DateTime.Now
            };
        }
    }
}
