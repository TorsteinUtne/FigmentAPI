using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Document
    {
       public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public string ContentAsBase64 { get; set; }

        public string DocumentCategory { get; set; }

        public DocumentCategoryType DocumentCategoryType { get; set; }
 
    }
}
