using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
        [ForeignKey("ProductType")]
        public Guid ProductTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        [ForeignKey("LedgerAccount")]
        public Guid FromAccount { get; set; }
        [ForeignKey("LedgerAccount")]
        public Guid ToAccount { get; set; }


        public DateTime Date {get; set;}
    }
}
