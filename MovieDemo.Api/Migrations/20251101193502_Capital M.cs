using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDemo.Api.Migrations
{
    /// <inheritdoc />
    public partial class CapitalM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_movies",
                table: "movies");

            migrationBuilder.RenameTable(
                name: "movies",
                newName: "Movies");

            migrationBuilder.RenameIndex(
                name: "IX_movies_Title_Year",
                table: "Movies",
                newName: "IX_Movies_Title_Year");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "movies");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_Title_Year",
                table: "movies",
                newName: "IX_movies_Title_Year");

            migrationBuilder.AddPrimaryKey(
                name: "PK_movies",
                table: "movies",
                column: "Id");
        }
    }
}
