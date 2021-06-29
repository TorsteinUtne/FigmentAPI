using PowerService.Data;
using System.Net.Http;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PowerService.Data.Models;
using PowerService.Data.Models.Queries;

namespace PowerService.Util
{
    public class HelpFunctions
    {
        internal static Guid GetOrganizationId()
        {
            return Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        }
        internal static Guid GetCurrentUserId()
        {
            //TODO Changed to logged in user using OAuth token
            return Guid.Parse("79DC839F-31AC-4641-A86C-50E44E12AF0A");
        }
        internal static string CheckForNullReference(string s)
        {
            if (s is null)
                return "";
            else
                return s;
        }
       
        internal static string GetUserNameFromId(Guid id, PowerServiceContext context)
        {
            var portalUserEntity =  context.PortalUsers.Find(id);
            if ( portalUserEntity == null)
            {
                return "Failed to retrieve owner of the record";
            }
            return portalUserEntity.FirstName + " " + portalUserEntity.LastName;
        }

        internal static void SetPropertyValue(Object model, String name, Object value)
        {
            var prop = model.GetType().GetProperties().SingleOrDefault(p => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (prop != null)
            {
                var typedValue = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(model, typedValue);
            }
        }

        internal static bool CheckIfValueIsEnum(object value, string path)
        {
            object enumValue;
            bool result = false;
            switch (path.ToLower())
            {
                case "accounttype":
                     result = Enum.TryParse(typeof(AccountTypes),  value.ToString(), false, out enumValue);
                    break;
                default:
                    result = Enum.TryParse(typeof(AccountTypes), value.ToString(), false, out enumValue);
                    break;
            }
            return result;
        }

        internal static string GetAllValuesFromEnumAsString(string path)
        {
            string values = "";
            switch (path.ToLower())
            {
                case "accounttype":
                    var enumValues = Enum.GetValues(typeof(AccountTypes)).Cast<AccountTypes>();
                    foreach (var enumValue in enumValues)
                    { values += enumValue.ToString()+ " | ";}
                    break;
            }
            return values.Remove(values.Length - 3);
        }

        internal static string ExtractQueryInfoForLogger(SearchParameter searchParameters)
        {
            var str = "";
            str += " | Search field: " + searchParameters.SearchField + " | " + "Search value: " +
                   searchParameters.SearchValue + " | " + "Page Number: " + searchParameters.PageNumber + " | " +
                   "Page Size: " + searchParameters.PageSize + " | " + "Order: " + searchParameters.Order + " | " +
                   "Sorting field: " + searchParameters.SortingField + " at " + DateTime.Now.ToString();

            return str;
        }

        internal static void CreateLogEntry(LogLevel logLevel, ILogger logger, string additionalMessage, int eventId, string path)
        {
            CreateLogEntry(logLevel, logger, null, additionalMessage, eventId, path);
        }

        internal static void CreateLogEntry(LogLevel logLevel, ILogger logger, Exception ex, string additionalMessage,  int eventId, string path)
        {
            if (!Startup.LoggingEnabled)
                return;
            EventId eId;
            string message = "PATH " + path + " at " + DateTime.Now;

            switch (eventId)
            {
                case 10001:
                    eId= new EventId(10001, "Record(s) retrieved " + additionalMessage);
                    break;
                case 10002:
                    eId = new EventId(10002, "Record patched " + additionalMessage);
                    break;
                case 10003:
                    eId = new EventId(10003, "Record created " + additionalMessage);
                    break;
                case 10004:
                    eId = new EventId(10004, "Record deleted " + additionalMessage);
                    break;
                case 30001:
                    eId = new EventId(30001, "Access Denied" + additionalMessage);
                    break;
                case 59001:
                    eId = new EventId(590001, "Malformed request object " + additionalMessage);
                    break;
                default:
                    eId = new EventId(0, "No EventId given " + additionalMessage);
                    break;
            }

            switch (logLevel)
            {
                case LogLevel.Information:
                    logger.LogInformation(eId, ex, message );
                    break;
                case LogLevel.Warning:
                    logger.LogWarning(eId, ex, message);
                    break;
                case LogLevel.Error:
                    logger.LogError(eId, ex, message);
                    break;
                case LogLevel.Critical:
                    logger.LogCritical(eId, ex, message);
                    break;
                case LogLevel.Trace:
                    logger.LogTrace(eId, ex, message);
                    break;
                case LogLevel.Debug:
                    logger.LogTrace(eId, ex, message);
                    break;
                default:
                    logger.LogTrace(eId, ex, message);
                    break;
            }
            ;
        }
    }
}
