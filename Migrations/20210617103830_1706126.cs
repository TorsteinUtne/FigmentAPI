using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Migrations
{
    public partial class _1706126 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthOId",
                table: "PortalUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LicenseTypes",
                keyColumn: "Id",
                keyValue: new Guid("602380a1-3750-4c98-8c2f-75eab7d9f19a"),
                column: "ValidTill",
                value: new DateTime(2022, 6, 17, 12, 38, 28, 686, DateTimeKind.Local).AddTicks(4637));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthOId",
                table: "PortalUsers");

            migrationBuilder.UpdateData(
                table: "LicenseTypes",
                keyColumn: "Id",
                keyValue: new Guid("602380a1-3750-4c98-8c2f-75eab7d9f19a"),
                column: "ValidTill",
                value: new DateTime(2022, 6, 17, 12, 16, 14, 83, DateTimeKind.Local).AddTicks(1417));
        }
    }
}
