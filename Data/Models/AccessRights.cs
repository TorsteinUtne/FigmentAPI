using System;

namespace PowerService.Data.Models
{
    public class AccessRights
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }

        public string Description {get;set;}

        public bool Enabled { get; set; }
    }
}