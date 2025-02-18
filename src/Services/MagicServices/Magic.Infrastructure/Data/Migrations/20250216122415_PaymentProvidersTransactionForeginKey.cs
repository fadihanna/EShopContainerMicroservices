using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaymentProvidersTransactionForeginKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Transactions",
                newName: "PaymentProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentProviderId",
                table: "Transactions",
                column: "PaymentProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PaymentProviders_PaymentProviderId",
                table: "Transactions",
                column: "PaymentProviderId",
                principalTable: "PaymentProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PaymentProviders_PaymentProviderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PaymentProviderId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "PaymentProviderId",
                table: "Transactions",
                newName: "PaymentMethod");
        }
    }
}
