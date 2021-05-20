using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Data.Migrations
{
    public partial class _110521_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_LicenseType_LicenseId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_LicenseId",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "Organization");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationLicenseId",
                table: "Organization",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationLicenseId",
                table: "Organization",
                column: "OrganizationLicenseId",
                unique: true,
                filter: "[OrganizationLicenseId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_LicenseType_OrganizationLicenseId",
                table: "Organization",
                column: "OrganizationLicenseId",
                principalTable: "LicenseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_LicenseType_OrganizationLicenseId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_OrganizationLicenseId",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "OrganizationLicenseId",
                table: "Organization");

            migrationBuilder.AddColumn<Guid>(
                name: "LicenseId",
                table: "Organization",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_LicenseId",
                table: "Organization",
                column: "LicenseId",
                unique: true,
                filter: "[LicenseId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_LicenseType_LicenseId",
                table: "Organization",
                column: "LicenseId",
                principalTable: "LicenseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
