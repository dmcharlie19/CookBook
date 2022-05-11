using BookReader.Infrastructure.Configurations;
using CookBookBackend.Core.Domain;
using CookBookBackend.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CookBookBackend.Infrastructure.Foundation
{
  public class CookBookDbContext : DbContext
  {
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<User> Users { get; set; }

    public CookBookDbContext( DbContextOptions options ) : base( options )
    {
      Database.EnsureCreated();
      Recipes = Set<Recipe>();
      Users = Set<User>();
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
      modelBuilder.ApplyConfiguration( new RecipeConfiguration() );
      modelBuilder.ApplyConfiguration( new UserConfiguration() );
    }
  }
}
