using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Provider.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class editservicesID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCharges_MasaryServiceslists_MasaryServiceslistId",
                table: "ServiceCharges");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceParameters_MasaryServiceslists_MasaryServiceslistId",
                table: "ServiceParameters");

            migrationBuilder.DropIndex(
                name: "IX_ServiceParameters_MasaryServiceslistId",
                table: "ServiceParameters");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCharges_MasaryServiceslistId",
                table: "ServiceCharges");

            migrationBuilder.DropColumn(
                name: "MasaryServiceslistId",
                table: "ServiceParameters");

            migrationBuilder.DropColumn(
                name: "MasaryServiceslistId",
                table: "ServiceCharges");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "MasaryServiceslists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceParameters_ServiceId",
                table: "ServiceParameters",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_ServiceId",
                table: "ServiceCharges",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_MasaryServiceslists_ServiceId",
                table: "ServiceCharges",
                column: "ServiceId",
                principalTable: "MasaryServiceslists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceParameters_MasaryServiceslists_ServiceId",
                table: "ServiceParameters",
                column: "ServiceId",
                principalTable: "MasaryServiceslists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCharges_MasaryServiceslists_ServiceId",
                table: "ServiceCharges");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceParameters_MasaryServiceslists_ServiceId",
                table: "ServiceParameters");

            migrationBuilder.DropIndex(
                name: "IX_ServiceParameters_ServiceId",
                table: "ServiceParameters");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCharges_ServiceId",
                table: "ServiceCharges");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "MasaryServiceslists");

            migrationBuilder.AddColumn<int>(
                name: "MasaryServiceslistId",
                table: "ServiceParameters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MasaryServiceslistId",
                table: "ServiceCharges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceParameters_MasaryServiceslistId",
                table: "ServiceParameters",
                column: "MasaryServiceslistId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_MasaryServiceslistId",
                table: "ServiceCharges",
                column: "MasaryServiceslistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCharges_MasaryServiceslists_MasaryServiceslistId",
                table: "ServiceCharges",
                column: "MasaryServiceslistId",
                principalTable: "MasaryServiceslists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceParameters_MasaryServiceslists_MasaryServiceslistId",
                table: "ServiceParameters",
                column: "MasaryServiceslistId",
                principalTable: "MasaryServiceslists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
