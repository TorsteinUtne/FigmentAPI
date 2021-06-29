using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
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

        #region BusinessRules
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

       //TODO : Implement softdelete using av base class with Bool IsDeleted and override DeleteChangesAsync

        //Implement a routine that then deletes the data using configurable data retention policies

        
        #endregion


        public DbSet<PowerService.Data.Models.Organization> Organizations { get; set; }
        public DbSet<PowerService.Data.Models.Inventory> Inventory { get; set; }
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

       // public DbSet<PowerService.Data.Models.Audit> Audits { get; set; }

        public DbSet<PowerService.Data.Models.Address> Addresses { get; set; }

        public DbSet<PowerService.Data.Models.Activity> Activities { get; set; }

        public DbSet<PowerService.Data.Models.Account> Accounts { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("PowerServiceContext", builder =>
        //    {
        //        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        //    });
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // #region Account

            // //TODO: Type, kontaktperson, adresse, avtaler, caser, aktiviteter, dokumenter
            // modelBuilder.Entity<Account>().HasData(new Account {Id = Guid.Parse("{C43F6A50-B605-43F6-A8DD-2367B6F12023}"), Name = "Min første kunde", Description="Denne oppføringen kan brukes til å knytte aktiviteter, dokumenter, og saker mot kunder, samt spore historikk",  OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}") });
            // #endregion


            // //TODO: Sender, receiver, activitytype, thread (Relations bewteen activities)
            // #region Activity
            // modelBuilder.Entity<Activity>().HasData(new Activity {Id = Guid.Parse("{8D7EC929-6FAE-484F-91A5-D1B6A5D3D667}"), Description= "This is a record that shows some sort of interaction between two parties", Name="The subject of the activity",  OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}") });
            // #endregion

            // //TODO: Addresselinjer adressetype, relatert til oppføring (Type RegardingObjectID)
            // #region Address
            // modelBuilder.Entity<Address>().HasData(new Address { Id = Guid.Parse("{FB33A4AA-067E-4D80-A30D-CF9CABC2E3B5}"), Name="Address for" , Description = "", OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}") });
            // #endregion

            // //Denne bør stå tom, 
            // #region Audit
            //// modelBuilder.Entity<Audit>().HasData(new Audit { });
            // #endregion
            // //TODO: Amount, receievanle, account, docment for receiet, date, status, etc
            // #region Billing
            // modelBuilder.Entity<Billing>().HasData(new Billing { Id = Guid.Parse("{EC734089-A981-460F-809C-3B534E52E8C3}")});
            // #endregion
            // //TODO: Related parties, owner, time/date, regardingobjectid - link to activities
            // #region Booking
            // modelBuilder.Entity<Booking>().HasData(new Booking { Id = Guid.Parse("{984313C9-137A-415B-B61A-096F27AF01E5}"),  Name = "Booking for service", Description="This field can be used to provide a more detailed description for the services offered" });
            // #endregion
            // //TODO: Received, connection to account, actitivites, status, etcs
            // #region Case
            // modelBuilder.Entity<Case>().HasData(new Case {Id = Guid.Parse("{27507AA5-8F60-49DB-98AC-F5DA0768615D}"), Name="Service request received", Description = "This field can be used to provide a more detailed description of the request", OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}"),  });
            // #endregion

            // #region Configuration
            //// modelBuilder.Entity<Configuration>().HasData(new Configuration { });
            // #endregion

            // #region ConfigurationItem
            //// modelBuilder.Entity<ConfigurationItem>().HasData(new ConfigurationItem { });
            // #endregion
            // //TODO: Link to contact and account. Sholuld thise have owner? Samtykketype må inn. Status også gitt/ikke gitt/truket
            // #region Consent
            // modelBuilder.Entity<Consent>().HasData(new Consent { Id = Guid.Parse("{AAEB9BB5-CDB4-49DE-84A7-7B758A04A0A9}"),  OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}"), Name= "Samtykke for person", Description="Hva innebærer samtykke" });
            // #endregion
            // //TODO: Full kontaktinfo: fornavn, etternavnm telefon, epost. Lenke til samtykke, account, activity, etc
            // #region Contact
            // modelBuilder.Entity<Contact>().HasData(new Contact { Id = Guid.Parse("{48477D0D-AFFC-47E7-8B8C-4454754627BF}"), OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}"), FirstName="Ola", LastName=" Nordmann" });
            // #endregion
            // //TODO: Innhold, filtype, endelse, tittel, beskrivelse, liste med metadata, koblingsfelt til andre oppføringer
            // #region Document
            // modelBuilder.Entity<Document>().HasData(new Document { Id = Guid.Parse("{ED20AC49-FF3A-4DCF-AFE3-AD22DA806CBF}"), OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}"),  });
            // #endregion

            // #region LicenseType
            // modelBuilder.Entity<LicenseType>().HasData(new LicenseType { Id = Guid.Parse("{602380A1-3750-4C98-8C2F-75EAB7D9F19A}"), License="MonthlySubscription", NumberOfUsers=20, ValidTill = DateTime.Now.AddYears(1), Description="This is a monthly subscription, paid for a full year"});
            // #endregion

            // #region Organization
            // modelBuilder.Entity<Organization>().HasData(new Organization { Id = Guid.Parse("{777A4315-7FD8-429D-9B9D-2F40FB67C13B}"),  OrganizationName="Konfigurativ"  });
            // #endregion
            // //TODO: Name, username, password(?)
            // #region PortalUser
            // modelBuilder.Entity<PortalUser>().HasData(new PortalUser { Id = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}")   });
            // modelBuilder.Entity<PortalUser>().HasData(new PortalUser { Id = Guid.Parse("{31653098-A8F5-43EF-90D8-E96908C2D0B7}") });
            // #endregion

            // //Todo. Name, type(relation) eller subscription, value, quantity, description, owner(nullable)
            // #region Product
            // modelBuilder.Entity<Product>().HasData(new Product { Id = Guid.Parse("{DCD711D0-43F4-465E-ACAB-7A34F6B200BF}") });
            // #endregion

            // //Todo: 1:N relation to product. Knytt til dokument
            // #region ProductType
            // modelBuilder.Entity<ProductType>().HasData(new ProductType { Id = Guid.Parse("{7D5EDB9B-3A0F-43E6-846B-DD643F20A9C9}") });
            // #endregion
            // //TOD: Remove user - change to account. Add  type, length, product, valid till, etc. Knytt til dokument
            // #region Subscription
            // modelBuilder.Entity<Subscription>().HasData(new Subscription { Id = Guid.Parse("{A1C3C58C-1921-4BEB-A44C-5AF73F6DF536}"),  OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}"), Name ="Forvaltnigsavtale", Description="Avtalen som regulerer vedlikeholdet av levert løsning "  });
            // #endregion
            // //TODO: Add amount, date, status, parties, direction, typeetc
            // #region Transaction
            // modelBuilder.Entity<Transaction>().HasData(new Transaction {Id = Guid.Parse("{1848C65B-C879-4226-8746-9ACC6E12E03A}"),Name = "Heading for transaction", Description = "Description for this transaction",  OwnerId = Guid.Parse("{79DC839F-31AC-4641-A86C-50E44E12AF0A}") });
            // #endregion
            // //modelBuilder.Entity<Post>(
            // //    entity =>
            // //    {
            // //        entity.HasOne(d => d.Blog)
            // //            .WithMany(p => p.Posts)
            // //            .HasForeignKey("BlogId");
            // //    });

            // //#region PostSeed
            // //modelBuilder.Entity<Post>().HasData(
            // //    new Post { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" });
            // //#endregion

            // //#region AnonymousPostSeed
            // //modelBuilder.Entity<Post>().HasData(
            // //    new { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" });
            // //#endregion

            // //#region OwnedTypeSeed
            // //modelBuilder.Entity<Post>().OwnsOne(p => p.AuthorName).HasData(
            // //    new { PostId = 1, First = "Andriy", Last = "Svyryd" },
            // //    new { PostId = 2, First = "Diego", Last = "Vega" });
            // //#endregion

            
        }
        //public static IQueryable<object> Set(this DbContext context, Type t)
        //{

        //    return (IQueryable<object>)context.GetType()
        //          .GetMethod("Set")?
        //          .MakeGenericMethod(t)
        //          .Invoke(context, null);

        //}
    }
}
