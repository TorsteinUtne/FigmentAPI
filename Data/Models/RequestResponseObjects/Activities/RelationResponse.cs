using System;

namespace PowerService.Data.Models.RequestResponseObjects
{
    public class RelationResponse
    {
        public RelationResponse()
        {
        }
        public Guid ActivityId { get; set; }
        public Guid ForeignKeyId { get; set; }
        public string ForeignKeyName { get; set; }
    }
}