using CookBook.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Infrastructure.Configurations
{
    public class UserLikeConfiguration : IEntityTypeConfiguration<UserLike>
    {
        public void Configure( EntityTypeBuilder<UserLike> builder )
        {
            builder.HasKey( x => x.Id );

            builder.HasOne( ul => ul.User )
              .WithMany( u => u.UserLikes )
              .HasForeignKey( ul => ul.UserId )
              .OnDelete( DeleteBehavior.NoAction );

        }
    }
}
