using System;

namespace PowerService.Data.Models
{
    public class ConfigurationItem
    {
     public Guid Id { get; set; }
        public string TableName { get; set; }
        public bool IsActivated { get; set; }
    }
}