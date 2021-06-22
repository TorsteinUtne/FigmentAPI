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
        public static void Sanitize<T>(this Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<T> document) where T : class
        {
            for (int i = document.Operations.Count - 1; i >= 0; i--)
            {
                string pathPropertyName = document.Operations[i].path.Split("/", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                if (typeof(T).GetProperties().Where(p => p.IsDefined(typeof(DoNotPatchAttribute), true) && string.Equals(p.Name, pathPropertyName, StringComparison.CurrentCultureIgnoreCase)).Any())
                {
                    // remove
                    document.Operations.RemoveAt(i);

                    //todo: log removal
                }
            }
        }

        internal static List<String> GetDisplayableColumns<T>()
        {
            return new List<String>() { "Name", "Description" }; //Må være 1:1 mellom modell og entity
        }
    }
}
