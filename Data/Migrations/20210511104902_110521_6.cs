using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Data.Migrations
{
    public partial class _110521_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_LicenseType_OrganizationLicenseId",
                table: "Organization");

            migrationBuilder.DropTable(
                name: "LicenseType");

            migrationBuilder.DropTable(
                name: "Billing");

            migrationBuilder.DropTable(
                name: "PortalUser");

            migrationBuilder.DropIndex(
                name: "IX_Organization_OrganizationLicenseId",
                table: "Organization");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Billing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortalUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicenseType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfUsers = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidTill = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseType_Billing_BillingId",
                        column: x => x.BillingId,
                        principalTable: "Billing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LicenseType_PortalUser_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "PortalUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationLicenseId",
                table: "Organization",
                column: "OrganizationLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_BillingId",
                table: "LicenseType",
                column: "BillingId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_OwnerId",
                table: "LicenseType",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_LicenseType_OrganizationLicenseId",
                table: "Organization",
                column: "OrganizationLicenseId",
                principalTable: "LicenseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
