using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Migrations
{
  public partial class AddFieldPersonCountInRecipe : Migration
  {
    protected override void Up( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.AddColumn<int>(
          name: "PersonCount",
          table: "Recipes",
          type: "int",
          nullable: false,
          defaultValue: 0 );
    }

    protected override void Down( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.DropColumn(
          name: "PersonCount",
          table: "Recipes" );
    }
  }
}
