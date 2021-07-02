using PowerService.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Util;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PowerService.Data.Models;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;


namespace PowerService.DAL.Context
{

    public class AttachmentResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MimeAttachmentType { get; set; }

        public string FileName { get; set; }
        public string FileExtension { get; set; }

        public string ContentAsBase64 { get; set; }

        public Guid? ActivityId { get; set; }
        public Guid? DocumentId { get; set; }



        public Response<AttachmentResponse> GeneratePatchResponse(JsonPatchDocument<AttachmentResponse> patch,
            Attachment updatedAttachment, string path, PowerServiceContext context)
        {
            var response = new Response<AttachmentResponse>
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
            response.Data = GetResponse(updatedAttachment.Id, context).Result.Value;
            response.Succeeded = true;
            return response;
        }

        public async Task<ActionResult<List<AttachmentResponse>>> GetResponses(Guid activityId, PowerServiceContext context)
        {
            //var documents = new List<Document>();
            var documents = await context.Documents.Where(doc => doc.ActivityId == activityId).ToListAsync();
            //An attachment is a document attached to an object...
            if (documents == null)
                return null;

            return documents.Select(doc => new AttachmentResponse
                {
                    Name = doc.Name,
                    FileName = doc.FileName,
                    Description = doc.Description,
                    MimeAttachmentType = HelpFunctions.DetermineMimeType(doc.FileExtension),
                    FileExtension = doc.FileExtension,
                    ContentAsBase64 = doc.ContentAsBase64,
                    DocumentId = doc.Id,
                    ActivityId = activityId
                })
                .ToList();

        }

        public async Task<ActionResult<AttachmentResponse>> GetResponse(Guid activityId, PowerServiceContext context)
        {
            //An attachment is a document attached to an object...
            var document = await context.Documents.FindAsync(activityId);
            if (document == null)
                return null;

            var response = new AttachmentResponse
            {

                Name = document.Name,
                FileName = document.FileName,
                Description = document.Description,
                MimeAttachmentType = MimeTypes.GetMimeType(document.FileExtension),
                FileExtension = document.FileExtension,
                ContentAsBase64 = document.ContentAsBase64,
                ActivityId = activityId,
                DocumentId = document.Id
            };
            return response;
        }

        
    }
}