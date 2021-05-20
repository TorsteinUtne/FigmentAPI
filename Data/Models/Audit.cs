using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Audit :OrganizationModel
    {
        public string FieldName { get; set; }
        public string TableName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedDate { get; set; }
        public PortalUser ChangedByUser { get; set; }


    }
}
