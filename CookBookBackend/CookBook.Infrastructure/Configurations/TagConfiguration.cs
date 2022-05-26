using CookBook.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Infrastructure.Configurations
{
  public class TagConfiguration : IEntityTypeConfiguration<Tag>
  {
    public void Configure( EntityTypeBuilder<Tag> builder )
    {
      builder.HasKey( x => x.Id );
      builder.Property( x => x.Name ).HasMaxLength( 1000 );
    }
  }
}
