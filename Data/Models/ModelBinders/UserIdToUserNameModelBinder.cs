using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PowerService.Util;

namespace PowerService.Data.Models.ModelBinders
{
    //public class UserIdToUserNameModelBinder : IModelBinder
    //{
    //    public Task BindModelAsync(ModelBindingContext bindingContext)
    //    {
    //        var model = Activator.CreateInstance(bindingContext.ModelMetadata.ModelType);
    //        foreach (var keyValue in bindingContext.ActionContext.HttpContext.Response.)
    //        {
    //            HelpFunctions.SetPropertyValue(model, keyValue.Key, keyValue.Value.FirstOrDefault());
    //        }
    //    }
}
