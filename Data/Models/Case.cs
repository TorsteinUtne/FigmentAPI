using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PowerService.Data.Models.RequestResponseObjects;

namespace PowerService.Data.Models
{
    public class Case
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }

        [ForeignKey("Account")]
        public Guid? AccountId { get; set; }

        [ForeignKey("Contact")]

        public Guid? ContactId { get; set; }

        public List<RelatedItem> RelatedItems { get; set; } //The list of otems the case revolves around

        public DateTime CreatedOn { get; private set; }

        public DateTime ModifiedOn { get; private set; }
        [ForeignKey("PortalUser")]
        public Guid? CreatedBy { get; private set; }
        [ForeignKey("PortalUser")]
        public Guid? ModifiedBy { get; private set; }

        public Status Status { get; set; }

        public Case(CaseRequest request, Guid ownerId, PowerServiceContext context)
        {

            Id = request.Id;
            Name = request.Name;
            Description = request.Description;
            OwnerId = ownerId;


        }
    }
}
