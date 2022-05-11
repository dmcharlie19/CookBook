using CookBookBackend.Core.Domain;

namespace CookBook.Application.Repositories
{
  public interface IUserRepository
  {
    void Add( User user );
    void Remove( User user );
    User Get( string login );
    User Get( int id );
  }
}
