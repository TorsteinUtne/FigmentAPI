using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Attributes;
using PowerService.Util;

namespace PowerService.Data.Models.RequestResponseObjects
{
    public class AddressRequest
    {
        [SwaggerIgnore]
        [DoNotPatch]
        public Guid Id { get; set; }
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 chars length", MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public string Description { get; set; }
        [DefaultValue("Billing")]
        [EnumAttribute]
        public string AddressType { get; set; }

        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string StreetLine3 { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    
        public async Task<ActionResult<AddressRequest>> GetRequest(Guid id, PowerServiceContext context)
        {
            var address = await context.Addresses.FindAsync(id);
            if (address == null)
                return null;
            var request = new AddressRequest
            {
                Id = id,
                AddressType = address.AddressType,
                Name = address.Name,
                Description = address.Description,
                City = address.City,
                Country = address.Country,
                StreetLine1 = address.StreetLine1,
                StreetLine2 = address.StreetLine2,
                StreetLine3 = address.StreetLine3,
                Zip = address.Zip
            };
            return request;
        }
    }
}
