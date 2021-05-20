using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PowerService.Data.Models;

namespace PowerService.Data
{
    public class PowerServiceContext : DbContext
    {
        public PowerServiceContext (DbContextOptions<PowerServiceContext> options)
            : base(options)
        {
        }

        public DbSet<PowerService.Data.Models.Organization> Organizations { get; set; }

        public DbSet<PowerService.Data.Models.Transaction> Transactions { get; set; }
        public DbSet<PowerService.Data.Models.Subscription> Subscriptions { get; set; }
        public DbSet<PowerService.Data.Models.ProductType> ProductTypes { get; set; }
        public DbSet<PowerService.Data.Models.Product> Products { get; set; }

        public DbSet<PowerService.Data.Models.PortalUser> PortalUsers { get; set; }

        public DbSet<PowerService.Data.Models.LicenseType> LicenseTypes { get; set; }
        public DbSet<PowerService.Data.Models.Document> Documents { get; set; }

        public DbSet<PowerService.Data.Models.Contact> Contacts { get; set; }

        public DbSet<PowerService.Data.Models.Consent> Consents { get; set; }

        public DbSet<PowerService.Data.Models.ConfigurationItem> ConfigurationItems { get; set; }

        public DbSet<PowerService.Data.Models.Configuration> Configurations { get; set; }

        public DbSet<PowerService.Data.Models.Case> Cases { get; set; }

        public DbSet<PowerService.Data.Models.Booking> Bookings { get; set; }

        public DbSet<PowerService.Data.Models.Billing> Billings { get; set; }

        public DbSet<PowerService.Data.Models.Audit> Audits { get; set; }

        public DbSet<PowerService.Data.Models.Address> Addresses { get; set; }

        public DbSet<PowerService.Data.Models.Activity> Activities { get; set; }

        public DbSet<PowerService.Data.Models.Account> Accounts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("PowerServiceContext", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            #region Account
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Activity
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Address
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Audit
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Billing
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Booking
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Case
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Configuration
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region ConfigurationItem
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Consent
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Contact
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Document
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region LisenceType
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Organization
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region PortalUser
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion


            #region Product
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region ProductType
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Subscription
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion

            #region Transaction
            modelBuilder.Entity<Account>().HasData(new Account { });
            #endregion
            //modelBuilder.Entity<Post>(
            //    entity =>
            //    {
            //        entity.HasOne(d => d.Blog)
            //            .WithMany(p => p.Posts)
            //            .HasForeignKey("BlogId");
            //    });

            //#region PostSeed
            //modelBuilder.Entity<Post>().HasData(
            //    new Post { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" });
            //#endregion

            //#region AnonymousPostSeed
            //modelBuilder.Entity<Post>().HasData(
            //    new { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" });
            //#endregion

            //#region OwnedTypeSeed
            //modelBuilder.Entity<Post>().OwnsOne(p => p.AuthorName).HasData(
            //    new { PostId = 1, First = "Andriy", Last = "Svyryd" },
            //    new { PostId = 2, First = "Diego", Last = "Vega" });
            //#endregion
        }
    }
}
