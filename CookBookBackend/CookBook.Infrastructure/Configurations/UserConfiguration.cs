using CookBook.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Infrastructure.Configurations
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure( EntityTypeBuilder<User> builder )
    {
      builder.HasKey( x => x.Id );

      builder.Property( x => x.Login ).HasMaxLength( 20 );
      builder.Property( x => x.Password ).HasMaxLength( 20 );
      builder.Property( x => x.Name ).HasMaxLength( 20 );
    }
  }
}
