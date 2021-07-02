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

    public class CaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


       
       
        public Response<CaseResponse> GeneratePatchResponse(JsonPatchDocument<CaseRequest> patch,
            Case updatedCase, string path, PowerServiceContext context)
        {
            var response = new Response<CaseResponse>
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
            response.Data = GetResponse(updatedCase.Id, context).Result.Value;
            response.Succeeded = true;
            return response;
        }

        public async Task<ActionResult<CaseResponse>> GetResponse(Guid id, PowerServiceContext context)
        {
            var cCase = await context.Cases.FindAsync(id);
            if (cCase == null)
                return null;
            var response = new CaseResponse
            {
                Name = cCase.Name,
                Description = cCase.Description,
                Id = id,
              
            };
            return response;
        }
      

    }
}