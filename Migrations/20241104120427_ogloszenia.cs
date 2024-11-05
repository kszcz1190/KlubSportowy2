using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlubSportowy.Migrations
{
    /// <inheritdoc />
    public partial class ogloszenia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OgloszeniaModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tresc = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataDodania = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataUsunieciaOgloszenia = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgloszeniaModel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZawodnikOgloszenie",
                columns: table => new
                {
                    ZawodnikId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OgloszenieId = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZawodnikOgloszenie", x => new { x.ZawodnikId, x.OgloszenieId });
                    table.ForeignKey(
                        name: "FK_ZawodnikOgloszenie_AspNetUsers_ZawodnikId",
                        column: x => x.ZawodnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZawodnikOgloszenie_OgloszeniaModel_OgloszenieId",
                        column: x => x.OgloszenieId,
                        principalTable: "OgloszeniaModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ZawodnikOgloszenie_OgloszenieId",
                table: "ZawodnikOgloszenie",
                column: "OgloszenieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZawodnikOgloszenie");

            migrationBuilder.DropTable(
                name: "OgloszeniaModel");
        }
    }
}
