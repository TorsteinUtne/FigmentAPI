using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Attributes;
using PowerService.Util;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PowerService.Data.Models.RequestResponseObjects
{
    public class AccountRequest
    {
        [SwaggerIgnore]
        [DoNotPatchAttribute]
        public Guid Id { get; set; }
        [DefaultValue("Customer")]
        [EnumAttribute]
        public string AccountType { get; set; }
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 chars length", MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public string Description { get; set; }
        [SwaggerIgnore]
        public string Owner { get; set; } //Displayfield only, not relevant for create
        public string OrganizationNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string HomePage { get; set; }

        public string NACECode { get; set; }

        public async Task<ActionResult<AccountRequest>> GetRequest(Guid id, PowerServiceContext context)
        {
            var account = await context.Accounts.FindAsync(id);
            if (account == null)
                return null;
            var request = new AccountRequest();
            request.Id = id;
            request.AccountType = account.AccountType.ToString();
            request.Name = account.Name;
            request.Description = account.Description;
            request.Owner = MappingFunctions.GetOwnerName(account.OwnerId, context);
            request.OrganizationNumber = account.OrganizationNumber;
            request.PhoneNumber = account.PhoneNumber;
            request.HomePage = account.HomePage;
            request.NACECode = account.NACECode;

            return request;
        }
    }
}
