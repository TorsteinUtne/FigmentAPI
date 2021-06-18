using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerService.Data.Models
{
    public class Billing
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Account")]
        public Guid? AccountId { get; set; } //The account thta  the billing is for

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
      
        public DateTime DueDate { get; set; }

        public float Amount { get;  } //Calculated from Items

        public string AccountNo { get; set; } //Account number to pay

        public string InvoiceNo { get; set; }

        public double KID { get; set; }
        public List<BillingItem> Items { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
    }
}