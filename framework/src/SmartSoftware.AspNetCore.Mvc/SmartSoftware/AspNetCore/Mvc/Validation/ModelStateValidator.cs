using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Validation;

namespace SmartSoftware.AspNetCore.Mvc.Validation;

public class ModelStateValidator : IModelStateValidator, ITransientDependency
{
    public virtual void Validate(ModelStateDictionary modelState)
    {
        var validationResult = new SmartSoftwareValidationResult();

        AddErrors(validationResult, modelState);

        if (validationResult.Errors.Any())
        {
            throw new SmartSoftwareValidationException(
                "ModelState is not valid! See ValidationErrors for details.",
                validationResult.Errors
            );
        }
    }

    public virtual void AddErrors(ISmartSoftwareValidationResult validationResult, ModelStateDictionary modelState)
    {
        if (modelState.IsValid)
        {
            return;
        }

        foreach (var state in modelState)
        {
            foreach (var error in state.Value.Errors)
            {
                validationResult.Errors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
            }
        }
    }
}
