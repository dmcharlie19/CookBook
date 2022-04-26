using CookBookBackend.Core.Domain;
using CookBookBackend.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CookBookBackend.Infrastructure.Foundation
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
      modelBuilder.ApplyConfiguration( new RecipeConfiguration() );
    }
  }
}
