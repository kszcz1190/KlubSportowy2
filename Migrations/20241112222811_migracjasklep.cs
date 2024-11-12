using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlubSportowy.Migrations
{
    /// <inheritdoc />
    public partial class migracjasklep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "RezerwacjaModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrzedmiotModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezerwacjaModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RezerwacjaModel_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RezerwacjaModel_PrzedmiotModel_PrzedmiotModelId",
                        column: x => x.PrzedmiotModelId,
                        principalTable: "PrzedmiotModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RezerwacjaModel_ApplicationUserId",
                table: "RezerwacjaModel",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RezerwacjaModel_PrzedmiotModelId",
                table: "RezerwacjaModel",
                column: "PrzedmiotModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RezerwacjaModel");

        }
    }
}
