using PowerService.Data;
using PowerService.Data.Attributes;
using PowerService.Data.Models;
using PowerService.Data.Models.FriendlyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var owner = context.PortalUsers.Find(ownerId.Value);
            return owner.FirstName + " " + owner.LastName;
        }

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
              
                    if (typeof(T).GetProperties().Where(p => p.IsDefined(typeof(DoNotPatchAttribute), true) && string.Equals(p.Name, pathPropertyName, StringComparison.CurrentCultureIgnoreCase)).Any())
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
                    if (typeof(T).GetProperties().Where(p => p.IsDefined(typeof(EnumAttribute), true) && string.Equals(p.Name, pathPropertyName, StringComparison.CurrentCultureIgnoreCase)).Any())
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
            //TODO - Bruk AccountRequest-objektet i stedet, skjul ID
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
    }
}
