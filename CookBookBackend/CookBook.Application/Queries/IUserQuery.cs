using CookBook.Application.Queries.Dto;

namespace CookBook.Application.Queries
{
    public interface IUserQuery
    {
        UserInfoDto GetUserInfo( int userId );
    }
}
