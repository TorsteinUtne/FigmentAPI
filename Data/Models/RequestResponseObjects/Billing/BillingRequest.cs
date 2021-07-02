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
    public class BillingRequest
    {
        [SwaggerIgnore]
        [DoNotPatch]
        public Guid Id { get; set; }
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 chars length", MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public string Description { get; set; }
      
        public DateTime DueDate { get; set; }
        [DoNotPatch]
        public DateTime FromDate { get; set; }
        [DoNotPatch]
        public Guid? AccountId { get; set; }
        [DoNotPatch]
        public string AccountNo { get; set; }
        [DoNotPatch]
        public float Amount { get; set; }
        [DoNotPatch]
        public float VAT { get; set; }
        [DoNotPatch]
        public string InvoiceNo { get; set; }

        public double Kid { get; set; }
        public List<BillingItem> Items { get; set; }

        [Enum]
        [DefaultValue("Draft")]
        public string Status { get; set; }
        public async Task<ActionResult<BillingRequest>> GetRequest(Guid id, PowerServiceContext context)
        {
            var billing = await context.Billings.FindAsync(id);
            if (billing == null)
                return null;
            var request = new BillingRequest
            {
                Id = id,
                Name = billing.Name,
                Description = billing.Description,
                DueDate = billing.DueDate,
                FromDate = billing.FromDate,
                AccountId = billing.AccountId,
                AccountNo = billing.AccountNo,
                Amount = billing.Amount,
                VAT = billing.VAT,
                InvoiceNo = billing.InvoiceNo,
                Kid = billing.Kid,
                Items = billing.Items,
                Status = billing.Status
            };
            return request;
        }
    }
}
