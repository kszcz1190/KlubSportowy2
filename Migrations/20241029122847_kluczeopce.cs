using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlubSportowy.Migrations
{
    /// <inheritdoc />
    public partial class kluczeopce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ZawodnikModel",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ZawodnikModel_ApplicationUserId",
                table: "ZawodnikModel",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ZawodnikModel_AspNetUsers_ApplicationUserId",
                table: "ZawodnikModel",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZawodnikModel_AspNetUsers_ApplicationUserId",
                table: "ZawodnikModel");

            migrationBuilder.DropIndex(
                name: "IX_ZawodnikModel_ApplicationUserId",
                table: "ZawodnikModel");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ZawodnikModel");
        }
    }
}
