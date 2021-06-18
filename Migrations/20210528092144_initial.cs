using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    emailAddress = table.Column<string>(nullable: true),
                    AccountId = table.Column<Guid>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true),
                    AddressId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganizationName = table.Column<string>(nullable: true),
                    OrganizationLicenseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortalUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TableName = table.Column<string>(nullable: true),
                    IsActivated = table.Column<bool>(nullable: false),
                    ConfigurationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItems_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true),
                    ContactId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consents_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LicenseTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    License = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ValidTill = table.Column<DateTime>(nullable: false),
                    NumberOfUsers = table.Column<int>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    LicenseTypeBillingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseTypes_Billings_LicenseTypeBillingId",
                        column: x => x.LicenseTypeBillingId,
                        principalTable: "Billings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LicenseTypes_PortalUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "PortalUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("c43f6a50-b605-43f6-a8dd-2367b6f12023"), "Denne oppføringen kan brukes til å knytte aktiviteter, dokumenter, og saker mot kunder, samt spore historikk", "Min første kunde", new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a") });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("8d7ec929-6fae-484f-91a5-d1b6a5d3d667"), "This is a record that shows some sort of interaction between two parties", "The subject of the activity", new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a") });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("fb33a4aa-067e-4d80-a30d-cf9cabc2e3b5"), "", "Address for", new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a") });

            migrationBuilder.InsertData(
                table: "Billings",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("ec734089-a981-460f-809c-3b534e52e8c3"), null, null, null });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("984313c9-137a-415b-b61a-096f27af01e5"), "This field can be used to provide a more detailed description for the services offered", "Booking for service", null });

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("27507aa5-8f60-49db-98ac-f5da0768615d"), "This field can be used to provide a more detailed description of the request", "Service request received", new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a") });

            migrationBuilder.InsertData(
                table: "Consents",
                columns: new[] { "Id", "ContactId", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("aaeb9bb5-cdb4-49de-84a7-7b758a04a0a9"), null, "Hva innebærer samtykke", "Samtykke for person", new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a") });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "AccountId", "AddressId", "FirstName", "LastName", "OwnerId", "PhoneNumber", "emailAddress" },
                values: new object[] { new Guid("48477d0d-affc-47e7-8b8c-4454754627bf"), null, null, "Ola", " Nordmann", new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a"), null, null });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("ed20ac49-ff3a-4dcf-afe3-ad22da806cbf"), null, null, new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a") });

            migrationBuilder.InsertData(
                table: "LicenseTypes",
                columns: new[] { "Id", "Description", "License", "LicenseTypeBillingId", "NumberOfUsers", "OwnerId", "ValidTill" },
                values: new object[] { new Guid("602380a1-3750-4c98-8c2f-75eab7d9f19a"), "This is a monthly subscription, paid for a full year", "MonthlySubscription", null, 20, null, new DateTime(2022, 5, 28, 11, 21, 43, 998, DateTimeKind.Local).AddTicks(5039) });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "OrganizationLicenseId", "OrganizationName" },
                values: new object[] { new Guid("777a4315-7fd8-429d-9b9d-2f40fb67c13b"), null, "Konfigurativ" });

            migrationBuilder.InsertData(
                table: "PortalUsers",
                column: "Id",
                values: new object[]
                {
                    new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a"),
                    new Guid("31653098-a8f5-43ef-90d8-e96908c2d0b7")
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                column: "Id",
                value: new Guid("7d5edb9b-3a0f-43e6-846b-dd643f20a9c9"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("dcd711d0-43f4-465e-acab-7a34f6b200bf"), null, null, null });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("a1c3c58c-1921-4beb-a44c-5af73f6df536"), "Avtalen som regulerer vedlikeholdet av levert løsning ", "Forvaltnigsavtale", new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a") });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[] { new Guid("1848c65b-c879-4226-8746-9acc6e12e03a"), "Description for this transaction", "Heading for transaction", new Guid("79dc839f-31ac-4641-a86c-50e44e12af0a") });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItems_ConfigurationId",
                table: "ConfigurationItems",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consents_ContactId",
                table: "Consents",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseTypes_LicenseTypeBillingId",
                table: "LicenseTypes",
                column: "LicenseTypeBillingId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseTypes_OwnerId",
                table: "LicenseTypes",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "ConfigurationItems");

            migrationBuilder.DropTable(
                name: "Consents");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "LicenseTypes");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "PortalUsers");
        }
    }
}
