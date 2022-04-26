using CookBookBackend.Domain;

namespace CookBookBackend.Repositories
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
