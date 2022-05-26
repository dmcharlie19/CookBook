using System.Collections.Generic;
using System.Linq;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Infrastructure.Foundation;

namespace CookBook.Infrastructure.Repositories
{
  public class TagRepository : ITagRepository
  {
    private readonly CookBookDbContext _dbContext;

    public TagRepository( CookBookDbContext dbContext )
    {
      _dbContext = dbContext;
    }
    public Tag Get( string tagName )
    {
      return _dbContext.Tags.FirstOrDefault( tag => tag.Name == tagName );
    }

    public void Add( Tag tag )
    {
      _dbContext.Tags.Add( tag );
    }

    public void AddRange( List<Tag> tags )
    {
      _dbContext.Tags.AddRange( tags );
    }
  }
}
