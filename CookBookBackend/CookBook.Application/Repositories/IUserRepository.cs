using CookBook.Core.Domain;

namespace CookBook.Application.Repositories
{
  public interface IUserRepository
  {
    void Add( User user );
    User Get( string login );
    User Get( int id );
  }
}
