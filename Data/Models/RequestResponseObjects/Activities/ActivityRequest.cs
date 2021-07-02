using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Attributes;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;
using PowerService.Util;

namespace PowerService.Data.Models.RequestResponseObjects
{
    public class ActivityRequest
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
        [SwaggerIgnore]
        public List<RelatedObjects> RelatedObjects { get; set; }
        [Enum]
        [DefaultValue("Incoming")]
        public string Direction { get; set; }

        public string Content { get; set; }//Base64
        [DoNotPatch]
        [SwaggerIgnore]
        public DateTime CreatedOn { get; set; }
        [DoNotPatch]
        [SwaggerIgnore]
        public DateTime? SentOn { get; set; }
        [DoNotPatch]
        [SwaggerIgnore]
        public DateTime? ReceivedOn { get; set; }
        [DoNotPatch]
        [SwaggerIgnore]
        public DateTime? ModifiedOn { get; set; }
        [SwaggerIgnore]
        public List<Attachment> Attachments { get; set; }
        [Enum]
        [DefaultValue("Open")]
        public string Status { get; set; }

        public async Task<ActionResult<ActivityRequest>> GetRequest(Guid id, PowerServiceContext context)
        {
            var activity = await context.Activities.FindAsync(id);
            if (activity == null)
                return null;
            var request = new ActivityRequest
            {
                Id = id,
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
                RelatedObjects = MappingFunctions.GenerateRelations<Activity>(activity),
                Name = activity.Name,
                Description = activity.Description
            
            };
            return request;
        }
    }
}
