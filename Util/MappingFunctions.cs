using PowerService.Data.Models;
using PowerService.Data.Models.FriendlyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Util
{
    public class MappingFunctions
    {
        internal static IRecord ApplyChangesToRecord(IRecord record, IModel model, Type recordType, Type modelType)
        {
            var recordToChange = (Account)record;
            var modelToReadChangesFrom = (AccountModel)model;



            return new IRecord();
        }
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
    }
}
