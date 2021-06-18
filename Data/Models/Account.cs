using PowerService.Data.Models.FriendlyModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Account : IRecord
    {
        public Account() { }
        public Account(AccountModel model, Guid ownerId)
        {
 

            Id = model.Id;
            AccountType = (AccountTypes)Enum.Parse(typeof(AccountTypes), model.AccountType); 
            Name = model.Name;
            Description = model.Description;
            OwnerId = ownerId;
        }
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     
        public Guid Id { get; set; }

        public AccountTypes AccountType { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
    }
}
