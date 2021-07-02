using System;
namespace PowerService.Data.Models.RequestResponseObjects
{
    public class RelationRequest
    {
        public Guid ActivityId { get; set; }
        public Guid ForeignKeyId { get; set; }
        public string ForeignKeyName { get; set; }
    }
}