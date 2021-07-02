using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PowerService.Data.Models.RequestResponseObjects;

namespace PowerService.Data.Models
{
    public class Booking
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
        [ForeignKey("Billing")]
        public Guid? BillingId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndTime { get; set; }

        [ForeignKey("Product")]
        public Guid? ProductId { get; set; } //The product or item booked

        public int Quantity { get; set; }

        public string BookingStatus { get; set; }
        public Booking(BookingRequest request, Guid ownerId, PowerServiceContext context)
        {

            Id = request.Id;
            Name = request.Name;
            Description = request.Description;
            OwnerId = ownerId;
            AccountId = request.AccountId;
            ContactId = request.ContactId;
            BillingId = request.BillingId;
            StartDate = request.StartDate;
            EndTime = request.EndTime;
            ProductId = request.ProductId;
            Quantity = request.Quantity;
            BookingStatus = request.Description;
        }
    }
}
