using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class LicenseType : OrganizationModel
    {
        public string License { get; set; }

        public string Description { get; set; }

        public DateTime ValidTill { get; set; }

        public int NumberOfUsers { get; set; }

        public PortalUser Owner { get; set; }

        public Guid? LicenseTypeBillingId { get; set; }
        public Billing LicenseTypeBilling { get; set; }
    }
}
