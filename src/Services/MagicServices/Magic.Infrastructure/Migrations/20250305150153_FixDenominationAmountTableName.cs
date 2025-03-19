using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDenominationAmountTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amounts_Denomination_DenominationId",
                table: "Amounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amounts",
                table: "Amounts");

            migrationBuilder.RenameTable(
                name: "Amounts",
                newName: "DenominationAmounts");

            migrationBuilder.RenameIndex(
                name: "IX_Amounts_DenominationId",
                table: "DenominationAmounts",
                newName: "IX_DenominationAmounts_DenominationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DenominationAmounts",
                table: "DenominationAmounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DenominationAmounts_Denomination_DenominationId",
                table: "DenominationAmounts",
                column: "DenominationId",
                principalTable: "Denomination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DenominationAmounts_Denomination_DenominationId",
                table: "DenominationAmounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DenominationAmounts",
                table: "DenominationAmounts");

            migrationBuilder.RenameTable(
                name: "DenominationAmounts",
                newName: "Amounts");

            migrationBuilder.RenameIndex(
                name: "IX_DenominationAmounts_DenominationId",
                table: "Amounts",
                newName: "IX_Amounts_DenominationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amounts",
                table: "Amounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Amounts_Denomination_DenominationId",
                table: "Amounts",
                column: "DenominationId",
                principalTable: "Denomination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
