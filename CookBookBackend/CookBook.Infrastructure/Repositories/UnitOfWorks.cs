namespace CookBookBackend.Storage
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
