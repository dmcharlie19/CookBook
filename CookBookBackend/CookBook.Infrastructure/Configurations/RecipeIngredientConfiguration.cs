using CookBook.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Infrastructure.Configurations
{
  public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
  {
    public void Configure( EntityTypeBuilder<RecipeIngredient> builder )
    {
      builder.HasKey( x => x.Id );

      builder.HasOne( ingredient => ingredient.Recipe )
        .WithMany( recipe => recipe.RecipeIngredients )
        .HasForeignKey( ingredient => ingredient.RecipeId );

      builder.Property( x => x.Title ).HasMaxLength( 200 );
      builder.Property( x => x.Content ).HasMaxLength( 1000 );
    }
  }
}
