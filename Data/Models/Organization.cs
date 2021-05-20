using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Organization: OrganizationModel
    {
  
        public string OrganizationName { get; set; }
      
        public Guid? OrganizationLicenseId { get; set; }
       // public LicenseType OrganizationLicense { get; set; }
    }
}
