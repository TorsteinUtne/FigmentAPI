using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Migrations
{
    public partial class current : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "FromAccount",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FromParty",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ToAccount",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ToParty",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Subscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "Subscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PriceExMva",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypeId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "PortalUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PortalUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "PortalUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "PortalUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PortalUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PortalUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentAsBase64",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentCategory",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentCategoryType",
                table: "Documents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConsentType",
                table: "Consents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsWithdrawn",
                table: "Consents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Cases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Cases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Cases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingStatus",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RegardingId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Billings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNo",
                table: "Billings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Billings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Billings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "Billings",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "KID",
                table: "Billings",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "Billings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AddressType",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetLine1",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetLine2",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetLine3",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Direction",
                table: "Activities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "FromPartyId",
                table: "Activities",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedOn",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegardingObjectId",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentOn",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Activities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ToPartyId",
                table: "Activities",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccessRights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    PortalUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRights_PortalUsers_PortalUserId",
                        column: x => x.PortalUserId,
                        principalTable: "PortalUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MimeAttachmentType = table.Column<string>(nullable: true),
                    FileExtension = table.Column<string>(nullable: true),
                    ContentAsBase64 = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true),
                    ActivityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillingItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    SalesPriceExVAT = table.Column<float>(nullable: false),
                    VATFree = table.Column<bool>(nullable: false),
                    BillingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingItem_Billings_BillingId",
                        column: x => x.BillingId,
                        principalTable: "Billings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ProductTypeId = table.Column<Guid>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    FromAccount = table.Column<Guid>(nullable: false),
                    ToAccount = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatedItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PrimaryItemId = table.Column<Guid>(nullable: false),
                    RelatedItemId = table.Column<Guid>(nullable: false),
                    CaseId = table.Column<Guid>(nullable: true),
                    SubscriptionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedItem_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedItem_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "LicenseTypes",
                keyColumn: "Id",
                keyValue: new Guid("602380a1-3750-4c98-8c2f-75eab7d9f19a"),
                column: "ValidTill",
                value: new DateTime(2022, 6, 11, 10, 14, 10, 907, DateTimeKind.Local).AddTicks(6388));

            migrationBuilder.CreateIndex(
                name: "IX_AccessRights_PortalUserId",
                table: "AccessRights",
                column: "PortalUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ActivityId",
                table: "Attachment",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingItem_BillingId",
                table: "BillingItem",
                column: "BillingId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_CaseId",
                table: "RelatedItem",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedItem_SubscriptionId",
                table: "RelatedItem",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRights");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "BillingItem");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "RelatedItem");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromAccount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromParty",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToAccount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToParty",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceExMva",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "PortalUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PortalUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PortalUsers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "PortalUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PortalUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PortalUsers");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ContentAsBase64",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentCategory",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentCategoryType",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ConsentType",
                table: "Consents");

            migrationBuilder.DropColumn(
                name: "IsWithdrawn",
                table: "Consents");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingStatus",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RegardingId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "KID",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StreetLine1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StreetLine2",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StreetLine3",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FromPartyId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ReceivedOn",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "RegardingObjectId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "SentOn",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ToPartyId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "LicenseTypes",
                keyColumn: "Id",
                keyValue: new Guid("602380a1-3750-4c98-8c2f-75eab7d9f19a"),
                column: "ValidTill",
                value: new DateTime(2022, 5, 28, 11, 21, 43, 998, DateTimeKind.Local).AddTicks(5039));
        }
    }
}
