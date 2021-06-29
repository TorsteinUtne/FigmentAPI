using System;
namespace PowerService.Data.Models
{
    public enum AccountTypes
    {
        Supplier = 1,
        Customer = 2,
        Distributor=3,
        Retailer=4,
        Wholesaler=5,
        Partner = 6,
        Other = 7


    }
    public static class AccountTypesExtensions
    {
        public static bool Contains(this AccountTypes accountType, string toCheck, StringComparison comp)
        {
            return accountType.ToString().IndexOf(toCheck, comp) >= 0;
        }
    }
}