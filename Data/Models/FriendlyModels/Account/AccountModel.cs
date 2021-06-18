using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PowerService.Data.Models;
using PowerService.Util;

namespace PowerService.Data.Models.FriendlyModels
{
    public class AccountModelCreate : AccountModel 
    { 
        [SwaggerIgnore]
        public new Guid Id { get; set; }

        [Required]
        public new string Name { get; set; }

        [Required]
        public new string Owner { get; set; }
    }

    public class AccountModelRead : AccountModel { }
    public class AccountModelUpdate : AccountModel
    {
        public AccountModelUpdate(Account account, PowerServiceContext context) : base(account, context)
        {
           
        }
    }

    public class AccountModelDelete : AccountModel { } //Unnecessary, but included for consistency

    public class AccountModel : IModel
    {
        private PowerServiceContext _context;
        public AccountModel()
        {

        }
        public AccountModel(Account account, PowerServiceContext context)
        {
            _context = context;

            Id = account.Id;
            AccountType = account.AccountType.ToString();
            Name = account.Name;
            Description = account.Description;
            Owner = GetUserName(account.OwnerId.Value).Result; //TODO Check for empty value
        }
        private async Task<string> GetUserName(Guid ownerId)
        {
            var portalUserEntity = await _context.PortalUsers.FindAsync(ownerId);
            if (portalUserEntity == null)
            {
                return  "Failed to retrieve owner of the record";
            }
            return portalUserEntity.FirstName + " " + portalUserEntity.LastName;
        }
        public Guid Id { get; set; }
        [DefaultValue(AccountTypes.Customer)]
        public string AccountType { get; set; } 

        [StringLength(50, ErrorMessage ="Name must be between 2 and 50 chars length", MinimumLength =2)]
        public string Name { get; set; }
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public string Description { get; set; }

        public string Owner { get; set; } //Map Id to name
    }
}
