using CookBook.Application.Repositories;
using CookBook.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly CookBookDbContext _dbContext;

    public UnitOfWork( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }

    public void Commit()
    {
      _dbContext.SaveChanges();
    }
  }
}
