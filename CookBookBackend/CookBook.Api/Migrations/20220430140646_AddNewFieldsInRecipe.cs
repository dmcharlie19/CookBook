using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBookBackend.Migrations
{
    public partial class AddNewFieldsInRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeSet",
                table: "RecipeSet");

            migrationBuilder.RenameTable(
                name: "RecipeSet",
                newName: "Recipes");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Recipes",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Recipes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoritesCount",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FavoritesCount",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "RecipeSet");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "RecipeSet",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "RecipeSet",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeSet",
                table: "RecipeSet",
                column: "Id");
        }
    }
}
