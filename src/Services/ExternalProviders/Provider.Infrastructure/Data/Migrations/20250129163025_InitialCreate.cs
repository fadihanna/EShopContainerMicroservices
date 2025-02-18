using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Provider.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasaryServiceslists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinValue = table.Column<double>(type: "float", nullable: false),
                    MaxValue = table.Column<double>(type: "float", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    InquiryRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasaryServiceslists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<double>(type: "float", nullable: false),
                    To = table.Column<double>(type: "float", nullable: false),
                    Charge = table.Column<double>(type: "float", nullable: false),
                    Slap = table.Column<double>(type: "float", nullable: false),
                    Percentage = table.Column<bool>(type: "bit", nullable: false),
                    MasaryServiceslistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCharges_MasaryServiceslists_MasaryServiceslistId",
                        column: x => x.MasaryServiceslistId,
                        principalTable: "MasaryServiceslists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    ParameterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<bool>(type: "bit", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinLength = table.Column<int>(type: "int", nullable: false),
                    MaxLength = table.Column<int>(type: "int", nullable: false),
                    ConfirmRequired = table.Column<bool>(type: "bit", nullable: false),
                    MasaryServiceslistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceParameters_MasaryServiceslists_MasaryServiceslistId",
                        column: x => x.MasaryServiceslistId,
                        principalTable: "MasaryServiceslists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_MasaryServiceslistId",
                table: "ServiceCharges",
                column: "MasaryServiceslistId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceParameters_MasaryServiceslistId",
                table: "ServiceParameters",
                column: "MasaryServiceslistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCharges");

            migrationBuilder.DropTable(
                name: "ServiceParameters");

            migrationBuilder.DropTable(
                name: "MasaryServiceslists");
        }
    }
}
