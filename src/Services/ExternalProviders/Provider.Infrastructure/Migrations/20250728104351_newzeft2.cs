using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Provider.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newzeft2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DamenServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamenServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FawryBillers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSupportPartialPay = table.Column<bool>(type: "bit", nullable: false),
                    IsSupportOverPay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FawryBillers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DamenAmount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangeMin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RangeMax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsUserEntered = table.Column<bool>(type: "bit", nullable: false),
                    CalculateFee = table.Column<bool>(type: "bit", nullable: false),
                    Credit = table.Column<bool>(type: "bit", nullable: false),
                    Replace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DamenServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamenAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamenAmount_DamenServices_DamenServiceId",
                        column: x => x.DamenServiceId,
                        principalTable: "DamenServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamenServiceCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DamenServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamenServiceCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamenServiceCharges_DamenServices_DamenServiceId",
                        column: x => x.DamenServiceId,
                        principalTable: "DamenServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamenServiceFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxLen = table.Column<int>(type: "int", nullable: true),
                    MinLen = table.Column<int>(type: "int", nullable: true),
                    DefaultVal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: true),
                    IsPassword = table.Column<bool>(type: "bit", nullable: true),
                    Confirmed = table.Column<bool>(type: "bit", nullable: true),
                    DamenServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamenServiceFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamenServiceFields_DamenServices_DamenServiceId",
                        column: x => x.DamenServiceId,
                        principalTable: "DamenServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamenServiceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    DamenServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamenServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamenServiceRequests_DamenServices_DamenServiceId",
                        column: x => x.DamenServiceId,
                        principalTable: "DamenServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FawryBillerInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PmtType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportPartialPay = table.Column<bool>(type: "bit", nullable: false),
                    SupportOverPay = table.Column<bool>(type: "bit", nullable: false),
                    SupportInquiry = table.Column<bool>(type: "bit", nullable: false),
                    SupportReversal = table.Column<bool>(type: "bit", nullable: false),
                    BillerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FawryBillerInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FawryBillerInfos_FawryBillers_BillerId",
                        column: x => x.BillerId,
                        principalTable: "FawryBillers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamenServiceChargeSlide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScValueType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ScValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DamenServiceChargeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamenServiceChargeSlide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamenServiceChargeSlide_DamenServiceCharges_DamenServiceChargeId",
                        column: x => x.DamenServiceChargeId,
                        principalTable: "DamenServiceCharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamenServiceRequestInputField",
                columns: table => new
                {
                    DamenServiceRequestId = table.Column<int>(type: "int", nullable: false),
                    DataFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamenServiceRequestInputField", x => new { x.DamenServiceRequestId, x.DataFieldId });
                    table.ForeignKey(
                        name: "FK_DamenServiceRequestInputField_DamenServiceFields_DataFieldId",
                        column: x => x.DataFieldId,
                        principalTable: "DamenServiceFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DamenServiceRequestInputField_DamenServiceRequests_DamenServiceRequestId",
                        column: x => x.DamenServiceRequestId,
                        principalTable: "DamenServiceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DamenServiceRequestOutputField",
                columns: table => new
                {
                    DamenServiceRequestId = table.Column<int>(type: "int", nullable: false),
                    DataFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamenServiceRequestOutputField", x => new { x.DamenServiceRequestId, x.DataFieldId });
                    table.ForeignKey(
                        name: "FK_DamenServiceRequestOutputField_DamenServiceFields_DataFieldId",
                        column: x => x.DataFieldId,
                        principalTable: "DamenServiceFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DamenServiceRequestOutputField_DamenServiceRequests_DamenServiceRequestId",
                        column: x => x.DamenServiceRequestId,
                        principalTable: "DamenServiceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FawryPaymentItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillerInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FawryPaymentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FawryPaymentItems_FawryBillerInfos_BillerInfoId",
                        column: x => x.BillerInfoId,
                        principalTable: "FawryBillerInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCharge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillerInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCharge_FawryBillerInfos_BillerInfoId",
                        column: x => x.BillerInfoId,
                        principalTable: "FawryBillerInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FawryServiceChargeTier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    To = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Charge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    ServiceChargeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FawryServiceChargeTier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FawryServiceChargeTier_ServiceCharge_ServiceChargeId",
                        column: x => x.ServiceChargeId,
                        principalTable: "ServiceCharge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamenAmount_DamenServiceId",
                table: "DamenAmount",
                column: "DamenServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DamenServiceCharges_DamenServiceId",
                table: "DamenServiceCharges",
                column: "DamenServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DamenServiceChargeSlide_DamenServiceChargeId",
                table: "DamenServiceChargeSlide",
                column: "DamenServiceChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_DamenServiceFields_DamenServiceId",
                table: "DamenServiceFields",
                column: "DamenServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DamenServiceRequestInputField_DataFieldId",
                table: "DamenServiceRequestInputField",
                column: "DataFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_DamenServiceRequestOutputField_DataFieldId",
                table: "DamenServiceRequestOutputField",
                column: "DataFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_DamenServiceRequests_DamenServiceId",
                table: "DamenServiceRequests",
                column: "DamenServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FawryBillerInfos_BillerId",
                table: "FawryBillerInfos",
                column: "BillerId");

            migrationBuilder.CreateIndex(
                name: "IX_FawryPaymentItems_BillerInfoId",
                table: "FawryPaymentItems",
                column: "BillerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FawryServiceChargeTier_ServiceChargeId",
                table: "FawryServiceChargeTier",
                column: "ServiceChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharge_BillerInfoId",
                table: "ServiceCharge",
                column: "BillerInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamenAmount");

            migrationBuilder.DropTable(
                name: "DamenServiceChargeSlide");

            migrationBuilder.DropTable(
                name: "DamenServiceRequestInputField");

            migrationBuilder.DropTable(
                name: "DamenServiceRequestOutputField");

            migrationBuilder.DropTable(
                name: "FawryPaymentItems");

            migrationBuilder.DropTable(
                name: "FawryServiceChargeTier");

            migrationBuilder.DropTable(
                name: "DamenServiceCharges");

            migrationBuilder.DropTable(
                name: "DamenServiceFields");

            migrationBuilder.DropTable(
                name: "DamenServiceRequests");

            migrationBuilder.DropTable(
                name: "ServiceCharge");

            migrationBuilder.DropTable(
                name: "DamenServices");

            migrationBuilder.DropTable(
                name: "FawryBillerInfos");

            migrationBuilder.DropTable(
                name: "FawryBillers");
        }
    }
}
