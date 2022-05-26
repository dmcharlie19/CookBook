using System.Collections.Generic;
using CookBook.Core.Domain;

namespace CookBook.Application.Repositories
{
  public interface ITagRecipeRepository
  {
    void Add( TagRecipe tag );

    void AddRange( List<TagRecipe> tags );
  }
}
