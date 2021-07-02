using PowerService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    public class BookingResponse
    {
        [DoNotPatch]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? OwnerId { get; set; }

        public Guid? AccountId { get; set; }

        public Guid? ContactId { get; set; }

        public Guid? BillingId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndTime { get; set; }

        public Guid? ProductId { get; set; } //The product or item booked

        public int Quantity { get; set; }
        [SwaggerIgnore]
        [Enum]
        [DefaultValue("Inquiry")]
        public string BookingStatus { get; set; }



        public Response<BookingResponse> GeneratePatchResponse(JsonPatchDocument<BookingRequest> patch,
            Booking updatedBooking, string path, PowerServiceContext context)
        {
            var response = new Response<BookingResponse>
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
            response.Data = GetResponse(updatedBooking.Id, context).Result.Value;
            response.Succeeded = true;
            return response;
        }

        public async Task<ActionResult<BookingResponse>> GetResponse(Guid id, PowerServiceContext context)
        {
            var booking = await context.Bookings.FindAsync(id);
            if (booking == null)
                return null;
            var response = new BookingResponse
            {
                Name = booking.Name,
                Description = booking.Description,
                Id = id,
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
            return response;
        }
      

    }
}