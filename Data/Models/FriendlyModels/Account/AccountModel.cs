using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PowerService.Data.Attributes;
using PowerService.Data.Models;
using PowerService.Util;

namespace PowerService.Data.Models.FriendlyModels
{
    public class AccountModel
    {
        private PowerServiceContext _context;
        [SwaggerIgnore]
        public  Guid Id { get; set; }
        [DefaultValue("Customer")]
        public  string AccountType { get; set; }
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 chars length", MinimumLength = 2)]
        public  string Name { get; set; }
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public  string Description { get; set; }
        [SwaggerIgnore]
        public  string Owner { get; set; } //Displayfield only, not relevant for create
        public string OrganizationNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string HomePage { get; set; }

        public string NACECode { get; set; }

        private List<Address> _addresses;
        private List<Activity> _activities;
        private List<Subscription> _subscriptions;
        private List<Case> _cases;
        private List<Contact> _contacts;
        private List<Billing> _billings;
        private List<Booking> _bookings;
        private List<Document> _documents;
        private List<Purchase> _purchases;
        public List<Address> Addresses { get { if (_addresses == null) { _addresses = new List<Address>(); } return _addresses; } set { _addresses = value; } }

        public List<Activity> Activities { get { if (_activities == null) { _activities = new List<Activity>(); } return _activities; } set { _activities = value; } }

        public List<Subscription> Subscriptions { get { if (_subscriptions == null) { _subscriptions = new List<Subscription>(); } return _subscriptions; } set { _subscriptions = value; } }

        public List<Case> Cases { get { if (_cases == null) { _cases = new List<Case>(); } return _cases; } set { _cases = value; } }

        public List<Contact> Contacts { get { if (_contacts == null) { _contacts = new List<Contact>(); } return _contacts; } set { _contacts = value; } }

        public List<Billing> Billings { get { if (_billings == null) { _billings = new List<Billing>(); } return _billings; } set { _billings = value; } }

        public List<Booking> Bookings { get { if (_bookings == null) { _bookings = new List<Booking>(); } return _bookings; } set { _bookings = value; } }

        public List<Document> Document { get { if (_documents == null) { _documents = new List<Document>(); } return _documents; } set { _documents = value; } }

        public List<Purchase> Purchases { get { if (_purchases == null) { _purchases = new List<Purchase>(); } return _purchases; } set { _purchases = value; } }
        public AccountModel() { }
        public AccountModel(Account account, PowerServiceContext context) 
        {
            _context = context;
            Id = account.Id;
            AccountType = account.AccountType.ToString();
            Name = account.Name;
            Description = account.Description;
            Owner = GetUserName(account.OwnerId.Value).Result;//TODO: Hanlde null values
            OrganizationNumber = account.OrganizationNumber;
            PhoneNumber = account.PhoneNumber;
            HomePage = account.HomePage;
            NACECode = account.HomePage;

            Addresses = GetAddressesForAccount(account.Id, _context);
            Activities = GetActivitiesForAccount(account.Id, _context);
            Subscriptions = GetSubscriptionsForAccount(account.Id, _context);
            Cases = GetCasesForAccount(account.Id, _context);
            Contacts = GetContactsForAccount(account.Id, _context);
            Billings = GetBillingsForAccount(account.Id, _context);
            Bookings = GetBookingsForAccount(account.Id, _context);
            Document = GetDocumentForAccount(account.Id, _context);
            Purchases = GetPurchasesForAccount(account.Id, _context);
        }
        private List<Address> GetAddressesForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Address>();
        }
        private List<Activity> GetActivitiesForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Activity>();
        }
        private List<Subscription> GetSubscriptionsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Subscription>();
        }
        private List<Case> GetCasesForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Case>();
        }
        private List<Contact> GetContactsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Contact>();
        }
        private List<Billing> GetBillingsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Billing>();
        }
        private List<Booking> GetBookingsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Booking>();
        }
        private List<Document> GetDocumentForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Document>();
        }

        private List<Purchase> GetPurchasesForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Purchase>();
        }

        internal async Task<string> GetUserName(Guid ownerId)
        {
            var portalUserEntity = await _context.PortalUsers.FindAsync(ownerId);
            if (portalUserEntity == null)
            {
                return "Failed to retrieve owner of the record";
            }
            return portalUserEntity.FirstName + " " + portalUserEntity.LastName;
        }
     
    }
   

   
}
