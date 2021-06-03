using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

class FormDataJsonBinder : IModelBinder
{
        public System.Threading.Tasks.Task BindModelAsync(ModelBindingContext bindingContext)
        {
                if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

                // Fetch the value of the argument by name and set it to the model state
                string fieldName = bindingContext.FieldName;
                var valueProviderResult = bindingContext.ValueProvider.GetValue(fieldName);
                if (valueProviderResult == ValueProviderResult.None) return System.Threading.Tasks.Task.CompletedTask;
                else bindingContext.ModelState.SetModelValue(fieldName, valueProviderResult);

                // Do nothing if the value is null or empty
                string value = valueProviderResult.FirstValue;
                if (string.IsNullOrEmpty(value)) return System.Threading.Tasks.Task.CompletedTask;

                try
                {
                        // Deserialize the provided value and set the binding result
                        object result = JsonConvert.DeserializeObject(value, bindingContext.ModelType);
                        bindingContext.Result = ModelBindingResult.Success(result);
                }
                catch (JsonException)
                {
                        bindingContext.Result = ModelBindingResult.Failed();
                }

                return System.Threading.Tasks.Task.CompletedTask;
        }
}