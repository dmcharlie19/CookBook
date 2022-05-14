using CookBook.Core.Domain;
using CookBook.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Infrastructure.Foundation
{
  public class CookBookDbContext : DbContext
  {
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeStep> RecipeSteps { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<User> Users { get; set; }

    public CookBookDbContext( DbContextOptions options ) : base( options )
    {
      Database.EnsureCreated();
      Recipes = Set<Recipe>();
      RecipeSteps = Set<RecipeStep>();
      RecipeIngredients = Set<RecipeIngredient>();

      Users = Set<User>();
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
      modelBuilder.ApplyConfiguration( new RecipeConfiguration() );
      modelBuilder.ApplyConfiguration( new UserConfiguration() );
    }
  }
}
