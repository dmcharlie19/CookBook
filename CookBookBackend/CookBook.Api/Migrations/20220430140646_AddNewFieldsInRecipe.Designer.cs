// <auto-generated />
using CookBook.Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CookBook.Migrations
{
  [DbContext( typeof( CookBookDbContext ) )]
  [Migration( "20220430140646_AddNewFieldsInRecipe" )]
  partial class AddNewFieldsInRecipe
  {
    protected override void BuildTargetModel( ModelBuilder modelBuilder )
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation( "ProductVersion", "6.0.4" )
          .HasAnnotation( "Relational:MaxIdentifierLength", 128 );

      SqlServerModelBuilderExtensions.UseIdentityColumns( modelBuilder, 1L, 1 );

      modelBuilder.Entity( "CookBook.Core.Domain.Recipe", b =>
           {
             b.Property<int>( "Id" )
                      .ValueGeneratedOnAdd()
                      .HasColumnType( "int" );

             SqlServerPropertyBuilderExtensions.UseIdentityColumn( b.Property<int>( "Id" ), 1L, 1 );

             b.Property<int>( "FavoritesCount" )
                      .HasColumnType( "int" );

             b.Property<int>( "LikesCount" )
                      .HasColumnType( "int" );

             b.Property<int>( "PreparingTime" )
                      .HasColumnType( "int" );

             b.Property<string>( "ShortDescription" )
                      .HasMaxLength( 200 )
                      .HasColumnType( "nvarchar(200)" );

             b.Property<string>( "Tags" )
                      .HasColumnType( "nvarchar(max)" );

             b.Property<string>( "Title" )
                      .HasMaxLength( 120 )
                      .HasColumnType( "nvarchar(120)" );

             b.HasKey( "Id" );

             b.ToTable( "Recipes" );
           } );
#pragma warning restore 612, 618
    }
  }
}
