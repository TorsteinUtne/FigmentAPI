using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Attributes;
using PowerService.Util;

namespace PowerService.Data.Models.RequestResponseObjects
{
    public class BookingRequest
    {
        [SwaggerIgnore]
        [DoNotPatch]
        public Guid Id { get; set; }
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 chars length", MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public string Description { get; set; }

        [SwaggerIgnore]
        public Guid? OwnerId { get; set; }

        [SwaggerIgnore]
        public Guid? AccountId { get; set; }
        [SwaggerIgnore]
        public Guid? ContactId { get; set; }
        [SwaggerIgnore]
        public Guid? BillingId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndTime { get; set; }


        public Guid? ProductId { get; set; } //The product or item booked

        public int Quantity { get; set; }
        [SwaggerIgnore]
        [Enum]
        [DefaultValue("Inquiry")]
        public string BookingStatus { get; set; }


        public async Task<ActionResult<BookingRequest>> GetRequest(Guid id, PowerServiceContext context)
        {
            var booking = await context.Bookings.FindAsync(id);
            if (booking == null)
                return null;
            var request = new BookingRequest
            {
                Id = id,
                Name = booking.Name,
                Description = booking.Description, 
                OwnerId = booking.OwnerId,
                AccountId = booking.AccountId,
                ContactId = booking.ContactId,
                BillingId = booking.BillingId,
                StartDate = booking.StartDate,
                EndTime = booking.EndTime,
                ProductId = booking.ProductId,
                Quantity = booking.Quantity,
                BookingStatus = booking.Description
            };
            return request;
        }
    }
}
