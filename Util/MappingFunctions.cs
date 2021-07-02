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
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Namotion.Reflection;
using NJsonSchema.Generation;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;

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

        internal static Guid GetPartyIdFromHandle(string fromHandle, PowerServiceContext context)
        {
            //TODO: Find contact based on handle: EMail address, phonenumber, some-uri or full name

            return Guid.NewGuid();

            //default Guid.Empty;
        }

        //TODO: Bør flyttes over i en egen DataService - helst generics
        public static async Task<List<Address>> GetAddressesForAccount(Guid id, PowerServiceContext context)
        {
            var addresses = await context.Addresses.Where(a => a.AccountId == id).ToListAsync();
            return addresses;
        }

        internal static Guid? ExtractRelations<T>(Type type, List<RelatedObjects> relatedObjects)
        {

            //Check if id is in related objects based on type - match on foreignkeyname
            return Guid.NewGuid();
        }

        internal static List<RelatedObjects> GenerateRelations<T>(object classType)
        {
            var propInfos = classType.GetType().GetProperties();
            var relatedObjects = new List<RelatedObjects>();
  
            var properties = propInfos.Where(propertyInfo => propertyInfo.PropertyType == typeof(Guid?))
                .Where(propertyInfo => propertyInfo.Name.ToLower() != "id");
            foreach (var property in properties)
            {
                var relObj = new RelatedObjects
                {
                    ForeignKeyName = property.Name,
                    ForeignKeyValue = (Guid?)property.GetValue(classType)
                };

                relatedObjects.Add(relObj);
            }

            return relatedObjects;
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
            var noSearchList = new List<string>
            {
                "attachments",
                "owner",
                "fromhandle",
                "tohandle",
                "createdon",
                "modifiedon",
                "receivedon",
                "senton",
                "relatedobjects"
            }; //TODO : Implement datetime search
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(Guid))
                    continue;
                if (noSearchList.Contains(property.Name.ToLower()))
                    continue;
               
                columnList.Add(property.Name);
            }

            return columnList;
        }

        public static string BuildSearchClause(SearchParameter searchParameters, List<string> searchFields,
            PowerServiceContext context)
        {
            var i = 0;

            var searchText = "";
            foreach (var searchField in searchFields)
            {
                var searchValue = "";

                searchText += searchField + ".Contains(\"" + searchValue + "\")";
                searchValue = searchParameters.SearchValue;
  

                i++;
                if (searchFields.Count > i)
                    searchText += " || ";
             }
            return searchText;
        }

        //internal static List<dynamic> AddPatchOperationsToDynamicList<T>(List<T> operations)
        //{
        //    var dynamicList = new List<dynamic>();
        //    string operation = "";
        //    foreach (var op in operations)
        //    {
        //        op.TryGetPropertyValue( "OperationType")
        //        operation += $" Operation: {op.OperationType}" + Environment.NewLine +
        //                     $"{op.@from}" + Environment.NewLine +
        //                     $"{op.path}" + Environment.NewLine +
        //                     $"{op.value}" + Environment.NewLine +
        //                     Environment.NewLine;
        //    }
        //}

        private static Guid GetGuidFromOwnerName(string searchValue, PowerServiceContext context)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                //TODO: Implement fuzzy search
                try
                {
                    string firstName = searchValue.Split(' ')[0];
                    string lastName = searchValue.Split(' ')[1]; //This might throw error
                    var ownerId = context.PortalUsers.First(pu => pu.FirstName == firstName && pu.LastName == lastName)
                        .Id; //TODO: Use email addy instead
                    return ownerId;
                }
                catch (Exception)
                {
                    throw new Exception("Owner not found. Make sure that name is exact match");
                }
            }

            return Guid.Empty;
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

        internal static void AddRelationToActivity(Activity activity, RelatedObjects relatedObject)
        {
            switch (relatedObject.ForeignKeyName.ToLower())
            {
                case "account":
                    activity.AccountId = relatedObject.ForeignKeyValue;
                    break;
                case "billing":
                    activity.BillingId = relatedObject.ForeignKeyValue;
                    break; ;
                case "booking":
                    activity.BookingId = relatedObject.ForeignKeyValue;
                    break; ;
                case "case":
                    activity.CaseId = relatedObject.ForeignKeyValue;
                    break; ;
                case "product":
                    activity.ProductId = relatedObject.ForeignKeyValue;
                    break; ;
                case "purchase":
                    activity.PurchaseId = relatedObject.ForeignKeyValue;
                    break; ;
                case "subscription":
                    activity.SubscriptionId = relatedObject.ForeignKeyValue;
                    break; ;
                case "transaction":
                    activity.TransactionId = relatedObject.ForeignKeyValue;
                    break; ;
                default:
                    break;
            }
        }

        public static string GetHandle(Guid activityFromPartyId, PowerServiceContext context)
        {
            return "Handle";
        }

        internal static void RemoveRelations(Activity activity, string foreignKeyName)
        {
            switch (foreignKeyName.ToLower())
            {
                case "account":
                    activity.AccountId = null;
                    break;
                case "billing":
                    activity.BillingId = null;
                    break; ;
                case "booking":
                    activity.BookingId = null;
                    break; ;
                case "case":
                    activity.CaseId = null;
                    break; ;
                case "product":
                    activity.ProductId = null;
                    break; ;
                case "purchase":
                    activity.PurchaseId = null;
                    break; ;
                case "subscription":
                    activity.SubscriptionId = null;
                    break; ;
                case "transaction":
                    activity.TransactionId = null;
                    break; ;
                default:
                    break;
            }
        }
    }
}
