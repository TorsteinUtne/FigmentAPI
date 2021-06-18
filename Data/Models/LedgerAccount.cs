using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Data.Models
{
    public class LedgerAccount
    {
        public Guid Id { get; }

        public Guid AccountId { get; private set; }

        public DateTime Created { get; private set; }

        public LedgerAccountType LedgerAccountType { get; private set; }

        public List<Inventory> Transactions { get; private set; }
    }
}
