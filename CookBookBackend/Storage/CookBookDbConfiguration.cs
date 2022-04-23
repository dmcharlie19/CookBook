using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CookBookBackend.Domain;

namespace CookBookBackend.Storage
{
  public class CookBookDbConfiguration : IEntityTypeConfiguration<Recipe>
  {
    public void Configure( EntityTypeBuilder<Recipe> builder )
    {
      builder.HasKey( x => x.Id );

      builder.Property( x => x.Title ).HasMaxLength( 300 );
    }
  }
}
