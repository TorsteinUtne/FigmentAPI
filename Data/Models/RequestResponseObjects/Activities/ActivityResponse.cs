using PowerService.Data;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Util;
using Microsoft.AspNetCore.JsonPatch;
using PowerService.Data.Attributes;
using PowerService.Data.Models;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;


namespace PowerService.DAL.Context.RequestResponseObjects
{

    public class ActivityResponse
    {
        [SwaggerIgnore]
        [DoNotPatch]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Owner { get; set; }

        public string FromHandle { get; set; }

        public string ToHandle { get; set; }
        //Brukes til å knytte en hvilken som helst aktivitet opp mot et objekt her
        public List<RelatedObjects> RelatedObjects { get; set; }
     
        public string Direction { get; set; }

        public string Content { get; set; }//Base64
        [DoNotPatch]
        public DateTime CreatedOn { get; set; }
        [DoNotPatch]
        public DateTime? SentOn { get; set; }
        [DoNotPatch]
        public DateTime? ReceivedOn { get; set; }
        [DoNotPatch]
        public DateTime? ModifiedOn { get; set; }

        public List<Attachment> Attachments { get; set; }
      
        public string Status { get; set; }




        public Response<ActivityResponse> GeneratePatchResponse(JsonPatchDocument<ActivityRequest> patch,
            Activity updatedActivity, string path, PowerServiceContext context)
        {
            var response = new Response<ActivityResponse>
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
            response.Data = GetResponse(updatedActivity.Id, context).Result.Value;
            response.Succeeded = true;
            return response;
        }

        public async Task<ActionResult<ActivityResponse>> GetResponse(Guid id, PowerServiceContext context)
        {
            var activity = await context.Activities.FindAsync(id);
            if (activity == null)
                return null;
            var response = new ActivityResponse
            {
                Name = activity.Name,
              
                Description = activity.Description,
                Id = id,

                RelatedObjects = MappingFunctions.GenerateRelations<Activity>(activity),

                Content = activity.Content,
                CreatedOn = activity.CreatedOn,
                Direction = activity.Direction.ToString(),
                FromHandle = MappingFunctions.GetHandle(activity.FromPartyId, context),
                ToHandle = MappingFunctions.GetHandle(activity.ToPartyId, context),
                ModifiedOn = activity.ModifiedOn,
                Owner = MappingFunctions.GetOwnerName(activity.OwnerId, context),
                ReceivedOn = activity.ReceivedOn,
                SentOn = activity.SentOn,
                Attachments = activity.Attachments,


            };
            return response;
        }

        public static implicit operator ActivityResponse(ActionResult<List<AttachmentResponse>> v)
        {
            throw new NotImplementedException();
        }
    }
}