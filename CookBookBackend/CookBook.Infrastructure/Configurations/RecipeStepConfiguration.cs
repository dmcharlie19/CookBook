using System.Reflection.Emit;
using CookBook.Core.Domain;
using CookBook.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Infrastructure.Configurations
{
  public class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
  {
    public void Configure( EntityTypeBuilder<RecipeStep> builder )
    {
      builder.HasKey( x => x.Id );

      builder.HasOne( p => p.Recipe )
        .WithMany( b => b.RecipeSteps )
        .HasForeignKey( p => p.RecipeId );

      builder.Property( x => x.Content ).HasMaxLength( 1000 );
    }
  }
}
