using System.Collections.Generic;
using CookBookBackend.Core.Domain;

namespace CookBookBackend.Application.Repositories
{
  public interface IRecipeRepository
  {
    List<Recipe> GetRecipes();
    Recipe? Get( int id );
    void Create( Recipe recipe );
    void Delete( int id );
    void Update( Recipe recipe );
  }
}
