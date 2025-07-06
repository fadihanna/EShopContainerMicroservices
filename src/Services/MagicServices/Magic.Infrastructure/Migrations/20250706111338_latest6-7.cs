using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class latest67 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillerCode",
                table: "DenominationProviderCode",
                newName: "ProviderCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderCode",
                table: "DenominationProviderCode",
                newName: "BillerCode");
        }
    }
}
