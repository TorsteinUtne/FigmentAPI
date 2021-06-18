using PowerService.DAL.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerService.Data.Models
{
    public class PortalUser 
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        [ForeignKey("Organizations")]
        public Guid OrganizationId { get; set; }

        public List<AccessRights> AccessRights { get; private set; }

        public string AuthOId { get; set; }
    }
}