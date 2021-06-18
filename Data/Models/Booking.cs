using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        public DateTime? StartDate { get; set; }

        public DateTime? EndTime { get; set; }

        public Guid? RegardingId { get; set; } //The product or item booked

        public int Quantity { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
