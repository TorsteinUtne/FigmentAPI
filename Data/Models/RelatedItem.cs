using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class RelatedItem
    {
        public Guid Id { get; set; } //unique ID for this relationsship

        public Guid PrimaryItemId { get; set; }

        public Guid RelatedItemId { get; set; }
    }
}
