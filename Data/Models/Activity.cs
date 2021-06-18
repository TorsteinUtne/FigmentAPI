using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
        [ForeignKey("Contacts")]
        public Guid FromPartyId { get; set; }
        [ForeignKey("Contacts")]
        public Guid ToPartyId { get; set; }
        //Brukes til å knytte en hvilken som helst aktivitet opp mot et objekt her
        public Guid? RegardingObjectId { get; set; }

        public Direction Direction { get; set; }

        public string Content { get; set; }//Base64

        public DateTime CreatedOn { get; set; }

        public DateTime? SentOn { get; set; }

        public DateTime? ReceivedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
       
        public List<Attachment> Attachments { get; set; }

        public Status Status { get; set; }
    }
}
