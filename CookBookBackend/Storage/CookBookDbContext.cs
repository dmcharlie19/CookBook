using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using CookBookBackend.Domain;

namespace CookBookBackend.Storage
{
  public class CookBookDbContext : DbContext
  {
    public DbSet<Recipe> RecipeSet { get; set; }

    public CookBookDbContext( DbContextOptions options ) : base( options )
    {
      Database.EnsureCreated();
      RecipeSet = Set<Recipe>();
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
      modelBuilder.ApplyConfiguration( new CookBookDbConfiguration() );
    }
  }
}
