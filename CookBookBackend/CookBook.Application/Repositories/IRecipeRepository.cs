using CookBook.Core.Domain;

namespace CookBook.Application.Repositories
{
  public interface IRecipeRepository
  {
    void Add( Recipe recipe );
  }
}
