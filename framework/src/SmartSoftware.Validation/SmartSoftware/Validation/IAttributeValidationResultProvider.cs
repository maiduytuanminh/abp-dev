using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.Validation;

public interface IAttributeValidationResultProvider
{
    ValidationResult? GetOrDefault(ValidationAttribute validationAttribute, object? validatingObject, ValidationContext validationContext);
}
