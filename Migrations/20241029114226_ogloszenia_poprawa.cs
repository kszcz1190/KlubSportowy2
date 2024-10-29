using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlubSportowy.Migrations
{
    /// <inheritdoc />
    public partial class ogloszenia_poprawa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListaZawodnikow",
                table: "OgloszeniaModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListaZawodnikow",
                table: "OgloszeniaModel",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
