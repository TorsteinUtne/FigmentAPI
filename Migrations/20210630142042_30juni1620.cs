using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Migrations
{
    public partial class _30juni1620 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegardingObjectId",
                table: "Activities");

            migrationBuilder.AlterColumn<string>(
                name: "AddressType",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Direction",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "BillingId",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CaseId",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CaseId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "AddressType",
                table: "Addresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Activities",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Direction",
                table: "Activities",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegardingObjectId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
