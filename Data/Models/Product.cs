using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }

        public int Stock { get; set; }

        [ForeignKey("Document")]
        public Guid? DocumentId { get; set; }

        public float? PriceExMva { get; set; }

        [ForeignKey ("ProductType")]
        public Guid? ProductTypeId { get; set; }
    }
}
