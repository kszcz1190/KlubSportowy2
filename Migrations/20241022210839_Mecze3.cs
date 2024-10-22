using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlubSportowy.Migrations
{
    /// <inheritdoc />
    public partial class Mecze3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZawodnikModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Imie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nazwisko = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Wiek = table.Column<int>(type: "int", nullable: false),
                    Kraj = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pozycja = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LacznaIloscGoli = table.Column<int>(type: "int", nullable: false),
                    LacznaIloscZoltychKartek = table.Column<int>(type: "int", nullable: false),
                    LacznaIloscCzerwonychKartek = table.Column<int>(type: "int", nullable: false),
                    LacznaIloscMeczyRozegranych = table.Column<int>(type: "int", nullable: false),
                    LacznaIloscMinutRozegranych = table.Column<int>(type: "int", nullable: false),
                    NumerZawodnika = table.Column<int>(type: "int", nullable: false),
                    MeczModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZawodnikModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZawodnikModel_MeczModel_MeczModelId",
                        column: x => x.MeczModelId,
                        principalTable: "MeczModel",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StatystykiZawodnikaMeczModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MeczModelId = table.Column<int>(type: "int", nullable: false),
                    ZawodnikModelId = table.Column<int>(type: "int", nullable: false),
                    IloscGoli = table.Column<int>(type: "int", nullable: false),
                    IloscZoltychKartek = table.Column<int>(type: "int", nullable: false),
                    IloscCzerwonychKartek = table.Column<int>(type: "int", nullable: false),
                    IloscMinutRozegranych = table.Column<int>(type: "int", nullable: false),
                    CzyZawodnikZagralWMeczu = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CzyKapitan = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Pozycja = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatystykiZawodnikaMeczModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatystykiZawodnikaMeczModel_MeczModel_MeczModelId",
                        column: x => x.MeczModelId,
                        principalTable: "MeczModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatystykiZawodnikaMeczModel_ZawodnikModel_ZawodnikModelId",
                        column: x => x.ZawodnikModelId,
                        principalTable: "ZawodnikModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StatystykiZawodnikaMeczModel_MeczModelId",
                table: "StatystykiZawodnikaMeczModel",
                column: "MeczModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StatystykiZawodnikaMeczModel_ZawodnikModelId",
                table: "StatystykiZawodnikaMeczModel",
                column: "ZawodnikModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ZawodnikModel_MeczModelId",
                table: "ZawodnikModel",
                column: "MeczModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatystykiZawodnikaMeczModel");

            migrationBuilder.DropTable(
                name: "ZawodnikModel");
        }
    }
}
