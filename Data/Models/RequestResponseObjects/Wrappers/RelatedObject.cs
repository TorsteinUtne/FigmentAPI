using System;

namespace PowerService.Data.Models.RequestResponseObjects.Wrappers
{
    public class RelatedObjects
    {
        public string ForeignKeyName { get; set; }
        public Guid? ForeignKeyValue { get; set; }
    }
}
