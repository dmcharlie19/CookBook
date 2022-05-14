using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Migrations
{
  public partial class AddTableUsers : Migration
  {
    protected override void Up( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            Id = table.Column<int>( type: "int", nullable: false )
                  .Annotation( "SqlServer:Identity", "1, 1" ),
            Login = table.Column<string>( type: "nvarchar(20)", maxLength: 20, nullable: true ),
            Password = table.Column<string>( type: "nvarchar(20)", maxLength: 20, nullable: true ),
            Name = table.Column<string>( type: "nvarchar(20)", maxLength: 20, nullable: true )
          },
          constraints: table =>
          {
            table.PrimaryKey( "PK_Users", x => x.Id );
          } );
    }

    protected override void Down( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.DropTable(
          name: "Users" );
    }
  }
}
