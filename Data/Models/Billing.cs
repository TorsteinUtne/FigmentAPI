using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Util;

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
        public float VAT { get; } //Calculated from Items
        public string AccountNo { get; set; } //Account number to pay

        public string InvoiceNo { get; set; }

        public string Status { get; set; }
        public double Kid { get; set; }
        public List<BillingItem> Items { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
        public Billing(BillingRequest request, Guid ownerId, PowerServiceContext context)
        {
           
            Id = request.Id;
            Name = request.Name;
            Description = request.Description;
            DueDate = request.DueDate;
            FromDate = request.FromDate;
            AccountId = request.AccountId;
            AccountNo = request.AccountNo;
            Amount = request.Amount;
            VAT = request.VAT;
            InvoiceNo = request.InvoiceNo;
            Kid = request.Kid;
            Items = request.Items;
            Status = request.Status;
        }
    }
}