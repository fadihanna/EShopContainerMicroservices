using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DenominationGroupddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInquiryRequired",
                table: "DenominationGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "DenominationGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DenominationGroupId",
                table: "Denomination",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Denomination_DenominationGroupId",
                table: "Denomination",
                column: "DenominationGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Denomination_DenominationGroups_DenominationGroupId",
                table: "Denomination",
                column: "DenominationGroupId",
                principalTable: "DenominationGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denomination_DenominationGroups_DenominationGroupId",
                table: "Denomination");

            migrationBuilder.DropIndex(
                name: "IX_Denomination_DenominationGroupId",
                table: "Denomination");

            migrationBuilder.DropColumn(
                name: "IsInquiryRequired",
                table: "DenominationGroups");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "DenominationGroups");

            migrationBuilder.DropColumn(
                name: "DenominationGroupId",
                table: "Denomination");
        }
    }
}
