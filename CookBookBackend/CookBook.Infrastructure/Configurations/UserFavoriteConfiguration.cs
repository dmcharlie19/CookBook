using CookBook.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Infrastructure.Configurations
{
    public class UserFavoriteConfiguration : IEntityTypeConfiguration<UserFavorite>
    {
        public void Configure( EntityTypeBuilder<UserFavorite> builder )
        {
            builder.HasKey( x => x.Id );

            builder.HasOne( uf => uf.User )
              .WithMany( u => u.UserFavorites )
              .HasForeignKey( uf => uf.UserId )
              .OnDelete( DeleteBehavior.NoAction );

        }
    }
}
