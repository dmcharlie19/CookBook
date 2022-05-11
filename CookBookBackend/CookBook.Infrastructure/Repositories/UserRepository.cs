using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Application.Repositories;
using CookBookBackend.Core.Domain;
using CookBookBackend.Infrastructure.Foundation;
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
      return _dbContext.Users.FirstOrDefault( user => user.Login == login );
    }

    public User Get( int id )
    {
      return _dbContext.Users.FirstOrDefault( user => user.Id == id );
    }

    public void Remove( User user )
    {
      throw new NotImplementedException();
    }
  }
}
