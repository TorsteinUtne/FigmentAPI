using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Consent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }

        public ConsentType ConsentType { get; set; }

        public bool IsWithdrawn { get; set; }
    }
}
