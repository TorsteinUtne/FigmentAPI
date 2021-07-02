﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PowerService.Data;

namespace PowerService.Migrations
{
    [DbContext(typeof(PowerServiceContext))]
    partial class PowerServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PowerService.Data.Models.AccessRights", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PortalUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PortalUserId");

                    b.ToTable("AccessRights");
                });

            modelBuilder.Entity("PowerService.Data.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NACECode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("PowerService.Data.Models.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BillingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FromPartyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PurchaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReceivedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SentOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ToPartyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("PowerService.Data.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StreetLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetLine3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("PowerService.Data.Models.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActivityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentAsBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MimeAttachmentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("PowerService.Data.Models.Billing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("KID")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Billings");
                });

            modelBuilder.Entity("PowerService.Data.Models.BillingItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BillingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SalesPriceExVAT")
                        .HasColumnType("real");

                    b.Property<bool>("VATFree")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BillingId");

                    b.ToTable("BillingItem");
                });

            modelBuilder.Entity("PowerService.Data.Models.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("RegardingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("PowerService.Data.Models.Case", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("PowerService.Data.Models.Configuration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("PowerService.Data.Models.ConfigurationItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("ConfigurationItems");
                });

            modelBuilder.Entity("PowerService.Data.Models.Consent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ConsentType")
                        .HasColumnType("int");

                    b.Property<Guid?>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsWithdrawn")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Consents");
                });

            modelBuilder.Entity("PowerService.Data.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("emailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("PowerService.Data.Models.Document", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActivityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentAsBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentCategoryType")
                        .HasColumnType("int");

                    b.Property<string>("FileExtension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("PowerService.Data.Models.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FromAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("ToAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("PowerService.Data.Models.LicenseType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("License")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LicenseTypeBillingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfUsers")
                        .HasColumnType("int");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ValidTill")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LicenseTypeBillingId");

                    b.HasIndex("OwnerId");

                    b.ToTable("LicenseTypes");
                });

            modelBuilder.Entity("PowerService.Data.Models.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Domain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrganizationLicenseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("PowerService.Data.Models.PortalUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthOId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PortalUsers");
                });

            modelBuilder.Entity("PowerService.Data.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("PriceExMva")
                        .HasColumnType("real");

                    b.Property<Guid?>("ProductTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PowerService.Data.Models.ProductType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("PowerService.Data.Models.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("PowerService.Data.Models.RelatedItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PrimaryItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RelatedItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("RelatedItem");
                });

            modelBuilder.Entity("PowerService.Data.Models.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("PowerService.Data.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FromAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FromParty")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ToAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ToParty")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("PowerService.Data.Models.AccessRights", b =>
                {
                    b.HasOne("PowerService.Data.Models.PortalUser", null)
                        .WithMany("AccessRights")
                        .HasForeignKey("PortalUserId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Activity", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Activities")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Address", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Addresses")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Attachment", b =>
                {
                    b.HasOne("PowerService.Data.Models.Activity", null)
                        .WithMany("Attachments")
                        .HasForeignKey("ActivityId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Billing", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Billings")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("PowerService.Data.Models.BillingItem", b =>
                {
                    b.HasOne("PowerService.Data.Models.Billing", null)
                        .WithMany("Items")
                        .HasForeignKey("BillingId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Booking", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Bookings")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Case", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Cases")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("PowerService.Data.Models.ConfigurationItem", b =>
                {
                    b.HasOne("PowerService.Data.Models.Configuration", null)
                        .WithMany("ConfigurationItems")
                        .HasForeignKey("ConfigurationId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Consent", b =>
                {
                    b.HasOne("PowerService.Data.Models.Contact", null)
                        .WithMany("Consents")
                        .HasForeignKey("ContactId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Contact", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Contacts")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Document", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Documents")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("PowerService.Data.Models.LicenseType", b =>
                {
                    b.HasOne("PowerService.Data.Models.Billing", "LicenseTypeBilling")
                        .WithMany()
                        .HasForeignKey("LicenseTypeBillingId");

                    b.HasOne("PowerService.Data.Models.PortalUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Purchase", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Purchases")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("PowerService.Data.Models.RelatedItem", b =>
                {
                    b.HasOne("PowerService.Data.Models.Case", null)
                        .WithMany("RelatedItems")
                        .HasForeignKey("CaseId");

                    b.HasOne("PowerService.Data.Models.Subscription", null)
                        .WithMany("Billings")
                        .HasForeignKey("SubscriptionId");
                });

            modelBuilder.Entity("PowerService.Data.Models.Subscription", b =>
                {
                    b.HasOne("PowerService.Data.Models.Account", null)
                        .WithMany("Subscriptions")
                        .HasForeignKey("AccountId");
                });
#pragma warning restore 612, 618
        }
    }
}
