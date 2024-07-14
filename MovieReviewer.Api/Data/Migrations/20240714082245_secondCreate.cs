using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewer.Api.Migrations
{
    /// <inheritdoc />
    public partial class secondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Reviews",
                newName: "IsDisabled");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ImdbId",
                table: "Movies",
                column: "ImdbId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_ImdbId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Reviews",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
