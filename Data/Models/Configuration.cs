using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Configuration
    {
        public Guid Id { get;set; }
        public List<ConfigurationItem> ConfigurationItems { get; set; }
    }
}
