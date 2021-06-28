using PowerService.Data;
using PowerService.Data.Attributes;
using PowerService.Data.Models;
using System.Linq;
using System.Threading.Tasks;
using PowerService.Data.Models.Queries;
using PowerService.Data.Models.RequestResponseObjects;
using System;
using System.Collections.Generic;
using System.Collections;
namespace PowerService.Util
{
    public static class MappingFunctions
    {
       
        public static void CopyValues<T>(T target, T source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }

        internal static string GetOwnerName(Guid? ownerId, PowerServiceContext context)
        {
            if (ownerId != null)
            {
                var owner = context.PortalUsers.Find(ownerId.Value);
                return owner.FirstName + " " + owner.LastName;
            }

            return null;
        }
        //TODO: Bør flyttes over i en egen DataService - helst generics
        internal static List<Address> GetAddressesForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Address>();
        }

        internal static List<Subscription> GetSubscriptionsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Subscription>();
        }

        internal static List<Billing> GetBillingsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Billing>();
        }

        internal static List<Document> GetDocumentsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Document>();
        }

        internal static List<Purchase> GetPurchasesForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Purchase>();
        }

        internal static List<Booking> GetBookingsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Booking>();
        }

        internal static List<Contact> GetContactsForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Contact>();
        }

        internal static List<Case> GetCasesForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Case>();
        }

        internal static List<Activity> GetActivitiesForAccount(Guid id, PowerServiceContext context)
        {
            return new List<Activity>();
        }

        public static void Sanitize<T>(this Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<T> document) where T : class
        {

            try
            {
                for (int i = document.Operations.Count - 1; i >= 0; i--)
                {
                    string pathPropertyName = document.Operations[i].path.Split("/", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
              
                    if (typeof(T).GetProperties().Any(p => p.IsDefined(typeof(DoNotPatchAttribute), true) && string.Equals(p.Name, pathPropertyName, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        // remove
                        if((document.Operations[i].op.ToLower() =="copy")|| (document.Operations[i].op.ToLower() == "test")) //Allow for copy and test
                        { continue; }
                        else
                        {
                            document.Operations.RemoveAt(i); //Remove
                            //Message user that ID wasn't changed
                            throw new Exception("Id's cannot be added, removed, copyed or replaced. Patch was not performed");
                        }
                    }
                    if (typeof(T).GetProperties().Any(p => p.IsDefined(typeof(EnumAttribute), true) && string.Equals(p.Name, pathPropertyName, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        // Validate that the value is within the range of the enum to ensure consistent values
                        if (!HelpFunctions.CheckIfValueIsEnum(document.Operations[i].value, pathPropertyName))
                        {
                            document.Operations.RemoveAt(i);
                            throw new Exception(pathPropertyName + " is not valid. Allowed values are: " + HelpFunctions.GetAllValuesFromEnumAsString(pathPropertyName) + ". Patch was not performed");
                            // remove
                            
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        internal static List<String> GetDisplayableColumns<T>()
        {
            var columnList = new List<string>();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(Guid))
                    continue;

                if (property.Name.ToLower() == "owner")
                    continue;
                columnList.Add(property.Name);
            }

            return columnList;
        }
        public static string BuildSearchClause(SearchParameter searchParameters, List<string> searchFields, string searchText)
        {
            var i = 0;
         

            foreach (var searchField in searchFields)
            {
                searchText += searchField + ".Contains(\"" + searchParameters.SearchValue + "\")";
                i++;
                if (searchFields.Count > i)
                    searchText += " || ";
            }
            return searchText;
        }
        public static string BuildOrderClause(SearchParameter searchParameters, List<string> columnNames, string defaultSortField)
        {
            var orderClause = "";

            List<string> orderColumns = new List<string>();
            try
            {
                var sortingColumns = new List<string>(searchParameters.SortingField.Split(';'));
                var displayColumns = MappingFunctions.GetDisplayableColumns<AccountRequest>();

                foreach (var sortColumn in sortingColumns)
                {
                    if (displayColumns.Contains(sortColumn))//Sjekker at den finnes i visningskolonner
                        orderColumns.Add(sortColumn);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            //Endre, denne sjekker nå på hele teksten. Bruk Any og match listen. Feilstavede kolonner må ignoreres
            if (columnNames.Any(x => orderColumns.Any(y => y == x))) 
            {
                //Hva med feilstavede kolonner? Blir de ignorert?

                foreach (string orderColumn in orderColumns)
                {
         
                        if (searchParameters.Order.ToLower() == "desc")
                            orderClause += orderColumn + " DESC,";
                        else
                            orderClause += orderColumn + " ASC,";
                }

                orderClause= orderClause.Remove(orderClause.Length-1);
            }
            else
            {
                orderClause = defaultSortField + " DESC";
            }
            return orderClause;
        }
       
    }
}
