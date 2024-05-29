using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.Validation;

public interface ISmartSoftwareValidationResult
{
    List<ValidationResult> Errors { get; }
}
