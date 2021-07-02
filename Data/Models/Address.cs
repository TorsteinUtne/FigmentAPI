using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PowerService.Data.Models.RequestResponseObjects;

namespace PowerService.Data.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string AddressType { get; set; }

        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string StreetLine3 { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [ForeignKey("PortalUser")] public Guid? OwnerId { get; set; }
        [ForeignKey("Account")] public Guid? AccountId { get; set; }

        public Address() { }
        public Address(AddressRequest request)
        {
            Id = request.Id;
            AddressType = request.AddressType;
            Name = request.Name;
            Description = request.Description;
            City = request.City;
            Country = request.Country;
            StreetLine1 = request.StreetLine1;
            StreetLine2 = request.StreetLine2;
            StreetLine3 = request.StreetLine3;
            Zip = request.Zip;
        }
    }
}
