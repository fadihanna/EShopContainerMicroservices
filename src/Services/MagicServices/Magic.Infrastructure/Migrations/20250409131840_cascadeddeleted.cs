using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadeddeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denomination_Service_ServiceId",
                table: "Denomination");

            migrationBuilder.AddForeignKey(
                name: "FK_Denomination_Service_ServiceId",
                table: "Denomination",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denomination_Service_ServiceId",
                table: "Denomination");

            migrationBuilder.AddForeignKey(
                name: "FK_Denomination_Service_ServiceId",
                table: "Denomination",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
