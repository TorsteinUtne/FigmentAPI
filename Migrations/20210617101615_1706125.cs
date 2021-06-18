using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Migrations
{
    public partial class _1706125 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "LicenseTypes",
                keyColumn: "Id",
                keyValue: new Guid("602380a1-3750-4c98-8c2f-75eab7d9f19a"),
                column: "ValidTill",
                value: new DateTime(2022, 6, 17, 12, 16, 14, 83, DateTimeKind.Local).AddTicks(1417));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "LicenseTypes",
                keyColumn: "Id",
                keyValue: new Guid("602380a1-3750-4c98-8c2f-75eab7d9f19a"),
                column: "ValidTill",
                value: new DateTime(2022, 6, 11, 10, 14, 10, 907, DateTimeKind.Local).AddTicks(6388));
        }
    }
}
