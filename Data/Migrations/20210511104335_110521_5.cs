using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Data.Migrations
{
    public partial class _110521_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billing_Organization_OrganizationId",
                table: "Billing");

            migrationBuilder.DropForeignKey(
                name: "FK_Organization_Organization_OrganizationId",
                table: "Organization");

            migrationBuilder.DropForeignKey(
                name: "FK_PortalUser_Organization_OrganizationId",
                table: "PortalUser");

            migrationBuilder.DropIndex(
                name: "IX_PortalUser_OrganizationId",
                table: "PortalUser");

            migrationBuilder.DropIndex(
                name: "IX_Organization_OrganizationId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_OrganizationLicenseId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Billing_OrganizationId",
                table: "Billing");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "PortalUser");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Billing");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "PortalUser",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedOn",
                table: "PortalUser",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Organization",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedOn",
                table: "Organization",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "LicenseType",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedOn",
                table: "LicenseType",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Billing",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedOn",
                table: "Billing",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationLicenseId",
                table: "Organization",
                column: "OrganizationLicenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organization_OrganizationLicenseId",
                table: "Organization");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "PortalUser",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedOn",
                table: "PortalUser",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "PortalUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Organization",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedOn",
                table: "Organization",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Organization",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "LicenseType",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedOn",
                table: "LicenseType",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Billing",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedOn",
                table: "Billing",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Billing",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortalUser_OrganizationId",
                table: "PortalUser",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationId",
                table: "Organization",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationLicenseId",
                table: "Organization",
                column: "OrganizationLicenseId",
                unique: true,
                filter: "[OrganizationLicenseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_OrganizationId",
                table: "Billing",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Billing_Organization_OrganizationId",
                table: "Billing",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_Organization_OrganizationId",
                table: "Organization",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PortalUser_Organization_OrganizationId",
                table: "PortalUser",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
