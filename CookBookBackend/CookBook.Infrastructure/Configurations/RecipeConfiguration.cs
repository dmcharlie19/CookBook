using CookBook.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Infrastructure.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure( EntityTypeBuilder<Recipe> builder )
        {
            builder.HasKey( x => x.Id );
        }
    }
}
