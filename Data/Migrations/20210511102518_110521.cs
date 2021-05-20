using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Data.Migrations
{
    public partial class _110521 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                table: "Organization");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "Organization",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Organization",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LicenseId",
                table: "Organization",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Billing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ChangedOn = table.Column<DateTime>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billing_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortalUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ChangedOn = table.Column<DateTime>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortalUser_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LicenseType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ChangedOn = table.Column<DateTime>(nullable: false),
                    License = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ValidTill = table.Column<DateTime>(nullable: false),
                    NumberOfUsers = table.Column<int>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    BillingId = table.Column<Guid>(nullable: true)
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
                name: "IX_Organization_LicenseId",
                table: "Organization",
                column: "LicenseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationId",
                table: "Organization",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_OrganizationId",
                table: "Billing",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_BillingId",
                table: "LicenseType",
                column: "BillingId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_OwnerId",
                table: "LicenseType",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PortalUser_OrganizationId",
                table: "PortalUser",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_LicenseType_LicenseId",
                table: "Organization",
                column: "LicenseId",
                principalTable: "LicenseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_Organization_OrganizationId",
                table: "Organization",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_LicenseType_LicenseId",
                table: "Organization");

            migrationBuilder.DropForeignKey(
                name: "FK_Organization_Organization_OrganizationId",
                table: "Organization");

            migrationBuilder.DropTable(
                name: "LicenseType");

            migrationBuilder.DropTable(
                name: "Billing");

            migrationBuilder.DropTable(
                name: "PortalUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_LicenseId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_OrganizationId",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "Organization");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "Organization",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "OrganizationId");
        }
    }
}
