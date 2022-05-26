using System.Collections.Generic;
using CookBook.Core.Domain;

namespace CookBook.Application.Repositories
{
  public interface ITagRepository
  {
    Tag Get( string tagName );

    void Add( Tag tag );

    void AddRange( List<Tag> tags );
  }
}
