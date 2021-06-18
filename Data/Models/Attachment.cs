using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerService.Data.Models
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string MimeAttachmentType { get; set; }

        public string FileExtension { get; set; }

        public string ContentAsBase64 { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }
    }
}