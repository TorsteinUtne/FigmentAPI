using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }

        public List<RelatedItem> Billings { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
