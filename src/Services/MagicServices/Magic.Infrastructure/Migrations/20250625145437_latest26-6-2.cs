using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class latest2662 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconName",
                table: "Denomination",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconName",
                table: "Denomination");
        }
    }
}
