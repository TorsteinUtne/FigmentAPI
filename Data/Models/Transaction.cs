using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("PortalUser")]
        public Guid? OwnerId { get; set; }

        public Guid? FromParty { get; set; }

        public Guid? ToParty { get; set; }

        public DateTime TransactionDate { get; set; }
        [ForeignKey("LedgerAccount")]
        public Guid FromAccount { get; set; }
        [ForeignKey("LedgerAccount")]
        public Guid ToAccount { get; set; }

        public float Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
