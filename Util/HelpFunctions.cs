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
    }
}
