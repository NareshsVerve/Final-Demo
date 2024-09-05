using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SiteInspectionWebApi.Helper
{
    public class EmptyStringToNullModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            // If the value is null or empty, we return null
            if (string.IsNullOrEmpty(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(value);
            }

            return Task.CompletedTask;
        }
    }

}
