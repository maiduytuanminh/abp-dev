using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSoftware.Validation;

namespace SmartSoftware.AspNetCore.Mvc.Validation;

public interface IModelStateValidator
{
    void Validate(ModelStateDictionary modelState);

    void AddErrors(ISmartSoftwareValidationResult validationResult, ModelStateDictionary modelState);
}
