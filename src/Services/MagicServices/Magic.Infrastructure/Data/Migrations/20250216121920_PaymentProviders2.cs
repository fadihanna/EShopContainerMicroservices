using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaymentProviders2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PaymentProviders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PaymentProviders");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "PaymentProviders");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "PaymentProviders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PaymentProviders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PaymentProviders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "PaymentProviders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "PaymentProviders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
