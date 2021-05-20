using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class UserModel : OrganizationModel
    {
      //  [ForeignKey("PortalUser")]
        public PortalUser? Owner { get; set; }
        //[ForeignKey("PortalUser")]
        //public PortalUser? CreatedBy { get; set; }
       
        //[ForeignKey("PortalUser")]
        //public PortalUser? ChangedBy { get; set; }
    }
}
