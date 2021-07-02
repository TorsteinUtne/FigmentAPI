using PowerService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Util;
using Microsoft.AspNetCore.JsonPatch;
using PowerService.Data.Attributes;
using PowerService.Data.Models;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;


namespace PowerService.DAL.Context
{

    public class BillingResponse
    {

        [DoNotPatch]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }
        [DoNotPatch]
        public DateTime FromDate { get; set; }
        public Guid? AccountId { get; set; }
        public string AccountNo { get; set; }
        public float Amount { get; set; }
        public float VAT { get; set; }
        public string InvoiceNo { get; set; }
        public double Kid { get; set; }
        public List<BillingItem> Items { get; set; }

        public string Status { get; set; }

        public Response<BillingResponse> GeneratePatchResponse(JsonPatchDocument<BillingRequest> patch,
            Billing updatedBilling, string path, PowerServiceContext context)
        {
            var response = new Response<BillingResponse>
            {
                Message = $"Object successfully patched at {path}." + Environment.NewLine
            };
            string operation = "";
            foreach (var op in patch.Operations)
            {
                operation += $" Operation: {op.OperationType}" + Environment.NewLine +
                             $"{op.@from}" + Environment.NewLine +
                             $"{op.path}" + Environment.NewLine +
                             $"{op.value}" + Environment.NewLine +
                             Environment.NewLine;
            }

            response.Message += operation;
            response.Data = GetResponse(updatedBilling.Id, context).Result.Value;
            response.Succeeded = true;
            return response;
        }

        public async Task<ActionResult<BillingResponse>> GetResponse(Guid id, PowerServiceContext context)
        {
            var billing = await context.Billings.FindAsync(id);
            if (billing == null)
                return null;
            var response = new BillingResponse
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
            return response;
        }
      

    }
}