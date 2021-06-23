using PowerService.Data.Models.FriendlyModels;
using PowerService.Data.Models.RequestResponseObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Account 
    {
        public Account() { }
        public Account(AccountRequest request, Guid ownerId)
        {

            Id = request.Id;
            AccountType = request.AccountType; 
            Name = request.Name;
            Description = request.Description;
            OwnerId = ownerId;
            OrganizationNumber = request.OrganizationNumber;
            PhoneNumber= request.PhoneNumber;
            HomePage = request.HomePage;
            NACECode= request.NACECode;
            //TODO: Fyll ut
        }
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     
        public Guid Id { get; set; }

        public string AccountType { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }

        public string OrganizationNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string HomePage { get; set; }

        public string NACECode { get; set; }
        
        public List<Address> Addresses { get; set; }

        public List<Activity> Activities { get; set; }

        public List<Subscription> Subscriptions { get; set; }
        public List<Case> Cases { get; set; }

        public List<Contact> Contacts { get; set; }

        public List<Billing> Billings { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<Document> Documents  { get; set; }

        public List<Purchase> Purchases { get; set; }

    }
}
