using System.ComponentModel.DataAnnotations;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Validation;

public class DefaultAttributeValidationResultProvider : IAttributeValidationResultProvider, ITransientDependency
{
    public virtual ValidationResult? GetOrDefault(ValidationAttribute validationAttribute, object? validatingObject, ValidationContext validationContext)
    {
        return validationAttribute.GetValidationResult(validatingObject, validationContext);
    }
}
