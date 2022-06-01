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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagRecipe> TagRecipes { get; set; }
        public DbSet<User> Users { get; set; }

        public CookBookDbContext( DbContextOptions options ) : base( options )
        {
            Recipes = Set<Recipe>();
            RecipeSteps = Set<RecipeStep>();
            RecipeIngredients = Set<RecipeIngredient>();
            Tags = Set<Tag>();
            TagRecipes = Set<TagRecipe>();

            Users = Set<User>();
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfiguration( new RecipeConfiguration() );
            modelBuilder.ApplyConfiguration( new RecipeStepConfiguration() );
            modelBuilder.ApplyConfiguration( new RecipeIngredientConfiguration() );
            modelBuilder.ApplyConfiguration( new TagConfiguration() );
            modelBuilder.ApplyConfiguration( new TagRecipeConfiguration() );

            modelBuilder.ApplyConfiguration( new UserConfiguration() );
        }
    }
}
