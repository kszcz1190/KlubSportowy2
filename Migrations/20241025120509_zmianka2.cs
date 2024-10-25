
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlubSportowy.Migrations
{
    /// <inheritdoc />
    public partial class zmianka2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZawodnikModel_MeczModel_MeczModelId",
                table: "ZawodnikModel");

            migrationBuilder.DropIndex(
                name: "IX_ZawodnikModel_MeczModelId",
                table: "ZawodnikModel");

            migrationBuilder.DropColumn(
                name: "MeczModelId",
                table: "ZawodnikModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeczModelId",
                table: "ZawodnikModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZawodnikModel_MeczModelId",
                table: "ZawodnikModel",
                column: "MeczModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZawodnikModel_MeczModel_MeczModelId",
                table: "ZawodnikModel",
                column: "MeczModelId",
                principalTable: "MeczModel",
                principalColumn: "Id");
        }
    }
}
