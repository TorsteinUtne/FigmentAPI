using PowerService.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class UserModel : OrganizationModel
    {

        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string MobilePhone { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool isActivated { get; set; }
        //  [ForeignKey("PortalUser")]
        //[SwaggerIgnoreAttribute]
        //public Guid OwnerId { get; set; }
        //[SwaggerIgnoreAttribute]
        //public PortalUser Owner { get; set; }
        //[ForeignKey("PortalUser")]
        //public PortalUser? CreatedBy { get; set; }

        //[ForeignKey("PortalUser")]
        //public PortalUser? ChangedBy { get; set; }
    }
}
