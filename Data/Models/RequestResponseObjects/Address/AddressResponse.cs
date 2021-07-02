using PowerService.Data;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Util;
using Microsoft.AspNetCore.JsonPatch;
using PowerService.Data.Models;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;


namespace PowerService.DAL.Context
{

    public class AddressResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string AddressType { get; set; }

        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string StreetLine3 { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Owner { get; set; }
        
        public List<RelatedObjects> RelatedObjects { get; set; }

       
       
        public Response<AddressResponse> GeneratePatchResponse(JsonPatchDocument<AddressRequest> patch,
            Address updatedAddress, string path, PowerServiceContext context)
        {
            var response = new Response<AddressResponse>
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
            response.Data = GetResponse(updatedAddress.Id, context).Result.Value;
            response.Succeeded = true;
            return response;
        }

        public async Task<ActionResult<AddressResponse>> GetResponse(Guid id, PowerServiceContext context)
        {
            var address = await context.Addresses.FindAsync(id);
            if (address == null)
                return null;
            var response = new AddressResponse
            {
                Name = address.Name,
                AddressType = address.AddressType, //Parse string value
                City = address.City,
                Zip = address.Zip,
                Country = address.Country,
                Description = address.Description,
                Id = id,
                Owner = MappingFunctions.GetOwnerName(address.OwnerId, context),
                StreetLine1 = address.StreetLine1,
                StreetLine2 = address.StreetLine2,
                StreetLine3 = address.StreetLine3,
                RelatedObjects = MappingFunctions.GenerateRelations<Address>(address)
            };
            return response;
        }
      

    }
}