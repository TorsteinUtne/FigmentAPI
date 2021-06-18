using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AddressType AddressType { get; set; }

        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string StreetLine3 { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
    }
}
