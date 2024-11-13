using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlubSportowy.Migrations
{
    /// <inheritdoc />
    public partial class zdjecieurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZdjecieUrl",
                table: "PrzedmiotModel",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZdjecieUrl",
                table: "PrzedmiotModel");
        }
    }
}
