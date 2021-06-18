using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerService.Util;
namespace PowerService.Data.Models.ModelBinders
{
    public class AccountModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var model = Activator.CreateInstance(bindingContext.ModelMetadata.ModelType);
            //Bind matching values from the   body//TODO USE A streamreader
            //foreach (var keyValue in bindingContext.ActionContext.HttpContext.Request.Body)
            //{
            //    HelpFunctions.SetPropertyValue(model, keyValue.Key, keyValue.Value.FirstOrDefault());
            //}

            //Bind matching value from the route  
            foreach (var routeValue in bindingContext.ActionContext.RouteData.Values)
            {
                HelpFunctions.SetPropertyValue(model, routeValue.Key, routeValue.Value);
            }


            //Return the model  
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
