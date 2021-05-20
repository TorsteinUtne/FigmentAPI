using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Data.Migrations
{
    public partial class _110521_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_LicenseType_LicenseId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_LicenseId",
                table: "Organization");

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseId",
                table: "Organization",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_LicenseType_LicenseId",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_LicenseId",
                table: "Organization");

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseId",
                table: "Organization",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_LicenseId",
                table: "Organization",
                column: "LicenseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_LicenseType_LicenseId",
                table: "Organization",
                column: "LicenseId",
                principalTable: "LicenseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
