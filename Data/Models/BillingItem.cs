
using System;
namespace PowerService.Data.Models
{
    public class BillingItem
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Quantity { get; set; }

        public float SalesPriceExVAT { get; set; }

        public bool VATFree { get; set; }
    }
}