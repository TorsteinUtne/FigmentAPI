using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Attributes;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;
using PowerService.Util;

namespace PowerService.Data.Models.RequestResponseObjects
{
    public class AttachmentRequest
    {
      [SwaggerIgnore]
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 chars length", MinimumLength = 2)]
        public string Name { get; set; }
        [SwaggerIgnore]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public string Description { get; set; }
        [DefaultValue("Billing")]
        [EnumAttribute]
        [SwaggerIgnore]
        public string MimeAttachmentType { get; set; }
        [SwaggerIgnore]
        public string FileName { get; set; }
        [SwaggerIgnore]
        public string FileExtension { get; set; }
        [SwaggerIgnore]
        public string ContentAsBase64 { get; set; }

        public Guid? ActivityId { get; set; }
        public Guid? DocumentId { get; set; }
        public async Task<ActionResult<AttachmentRequest>> GetRequest(Guid documentId,Guid activityId, PowerServiceContext context)
        {
            //Get all documents attached to that activity
            
            //An attachment is a document attached to an object...
            var document = await context.Documents.FindAsync(documentId);
            if (document == null)
                return null;
            var request = new AttachmentRequest
            {
    
                Name = document.Name,
                FileName = document.FileName,
                Description = document.Description,
                MimeAttachmentType  = HelpFunctions.DetermineMimeType(document.FileExtension),
                FileExtension = document.FileExtension,
                ContentAsBase64 = document.ContentAsBase64,
                DocumentId = document.Id,
                ActivityId = activityId
            };
            return request;
        }
    }
}
