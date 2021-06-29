using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Attributes;
using PowerService.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;

namespace PowerService.Data.Models.RequestResponseObjects
{
    public class AccountResponse
    {
        public AccountResponse()
        {
        }
        //    public AccountResponse(Guid id, PowerServiceContext context)
        //{
        ////Bygge Responsobjektet, populere det med data fra account i context basert på ID
        //var account = context.Accounts.Find(id);
        //Id = id;
        //AccountType = account.AccountType.ToString();
        //Name = account.Name;
        //Description = account.Description;
        //Owner = MappingFunctions.GetOwnerName(account.OwnerId, context);
        //OrganizationNumber = account.OrganizationNumber;
        //PhoneNumber = account.PhoneNumber;
        //HomePage = account.HomePage;
        //NACECode = account.NACECode;
        //Addresses = MappingFunctions.GetAddressesForAccount(id, context);
        //Activities = MappingFunctions.GetActivitiesForAccount(id, context);
        //Subscriptions = MappingFunctions.GetSubscriptionsForAccount(id, context);
        //Cases = MappingFunctions.GetCasesForAccount(id, context);
        //Contacts = MappingFunctions.GetContactsForAccount(id, context);
        //Billings = MappingFunctions.GetBillingsForAccount(id, context);
        //Bookings = MappingFunctions.GetBookingsForAccount(id, context);
        //Documents = MappingFunctions.GetDocumentsForAccount(id, context);
        //Purchases = MappingFunctions.GetPurchasesForAccount(id, context);

        //}
        public async Task<ActionResult<AccountResponse>> GetResponse(Guid id, PowerServiceContext context)
        {
            var account = await context.Accounts.FindAsync(id);
            if (account == null)
                return null;
            var response = new AccountResponse();
            response.Id = id;
            response.AccountType = account.AccountType.ToString();
            response.Name = account.Name;
            response.Description = account.Description;
            response.Owner = MappingFunctions.GetOwnerName(account.OwnerId, context);
            response.OrganizationNumber = account.OrganizationNumber;
            response.PhoneNumber = account.PhoneNumber;
            response.HomePage = account.HomePage;
            response.NACECode = account.NACECode;
            response.Addresses = MappingFunctions.GetAddressesForAccount(id, context);
            response.Activities = MappingFunctions.GetActivitiesForAccount(id, context);
            response.Subscriptions = MappingFunctions.GetSubscriptionsForAccount(id, context);
            response.Cases = MappingFunctions.GetCasesForAccount(id, context);
            response.Contacts = MappingFunctions.GetContactsForAccount(id, context);
            response.Billings = MappingFunctions.GetBillingsForAccount(id, context);
            response.Bookings = MappingFunctions.GetBookingsForAccount(id, context);
            response.Documents = MappingFunctions.GetDocumentsForAccount(id, context);
            response.Purchases = MappingFunctions.GetPurchasesForAccount(id, context);

            return response;
        }

        public Guid Id { get; set; }

        [DefaultValue("Customer")]
        [EnumAttribute]
        public string AccountType { get; set; }

        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 chars length", MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public string Description { get; set; }

        [SwaggerIgnore] public string Owner { get; set; } //Displayfield only, not relevant for create
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

        public List<Address> Addresses
        {
            get
            {
                if (_addresses == null)
                {
                    _addresses = new List<Address>();
                }

                return _addresses;
            }
            set { _addresses = value; }
        }

        public List<Activity> Activities
        {
            get
            {
                if (_activities == null)
                {
                    _activities = new List<Activity>();
                }

                return _activities;
            }
            set { _activities = value; }
        }

        public List<Subscription> Subscriptions
        {
            get
            {
                if (_subscriptions == null)
                {
                    _subscriptions = new List<Subscription>();
                }

                return _subscriptions;
            }
            set { _subscriptions = value; }
        }

        public List<Case> Cases
        {
            get
            {
                if (_cases == null)
                {
                    _cases = new List<Case>();
                }

                return _cases;
            }
            set { _cases = value; }
        }

        public List<Contact> Contacts
        {
            get
            {
                if (_contacts == null)
                {
                    _contacts = new List<Contact>();
                }

                return _contacts;
            }
            set { _contacts = value; }
        }

        public List<Billing> Billings
        {
            get
            {
                if (_billings == null)
                {
                    _billings = new List<Billing>();
                }

                return _billings;
            }
            set { _billings = value; }
        }

        public List<Booking> Bookings
        {
            get
            {
                if (_bookings == null)
                {
                    _bookings = new List<Booking>();
                }

                return _bookings;
            }
            set { _bookings = value; }
        }

        public List<Document> Documents
        {
            get
            {
                if (_documents == null)
                {
                    _documents = new List<Document>();
                }

                return _documents;
            }
            set { _documents = value; }
        }

        public List<Purchase> Purchases
        {
            get
            {
                if (_purchases == null)
                {
                    _purchases = new List<Purchase>();
                }

                return _purchases;
            }
            set { _purchases = value; }
        }

        public Response<AccountResponse> GeneratePatchResponse(JsonPatchDocument<AccountRequest> patch,
            Account updatedAccount, string path, PowerServiceContext context)
        {
            var response = new Response<AccountResponse>
            {
                Message = $"Object successfully patched at {path}." + Environment.NewLine
            };
            string operation = "";
            foreach (var op in patch.Operations)
            {
                operation += $" Operation: {op.OperationType}" + Environment.NewLine +
                             $"{op.@from}" + Environment.NewLine +
                             $"{op.path}" + Environment.NewLine +
                             $"{op.value}" + Environment.NewLine +
                             Environment.NewLine;
            }

            response.Message += operation;
            response.Data = GetResponse(updatedAccount.Id, context).Result.Value;
            response.Succeeded = true;
            return response;
        }
    }
}
