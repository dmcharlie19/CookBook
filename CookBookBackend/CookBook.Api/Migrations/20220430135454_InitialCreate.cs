using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Migrations
{
  public partial class InitialCreate : Migration
  {
    protected override void Up( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.CreateTable(
          name: "RecipeSet",
          columns: table => new
          {
            Id = table.Column<int>( type: "int", nullable: false )
                  .Annotation( "SqlServer:Identity", "1, 1" ),
            Title = table.Column<string>( type: "nvarchar(300)", maxLength: 300, nullable: true ),
            ShortDescription = table.Column<string>( type: "nvarchar(1000)", maxLength: 1000, nullable: true ),
            PreparingTime = table.Column<int>( type: "int", nullable: false )
          },
          constraints: table =>
          {
            table.PrimaryKey( "PK_RecipeSet", x => x.Id );
          } );
    }

    protected override void Down( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.DropTable(
          name: "RecipeSet" );
    }
  }
}
