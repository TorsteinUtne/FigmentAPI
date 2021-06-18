using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Organization
    {
        
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
      
        public Guid? OrganizationLicenseId { get; set; }

        public string Domain { get; set; }
        [ForeignKey("Account")]
        public Guid? AccountId { get; set; }
        // public LicenseType OrganizationLicense { get; set; }
        [ForeignKey("Organization")]
        public Guid? OrganizationId { get; set; }
    }
}
