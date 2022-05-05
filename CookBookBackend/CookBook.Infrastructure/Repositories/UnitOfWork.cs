using CookBookBackend.Application.Repositories;
using CookBookBackend.Infrastructure.Foundation;

namespace CookBookBackend.Infrastructure.Repositories
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
