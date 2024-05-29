using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.Validation;

public class SmartSoftwareValidationResult : ISmartSoftwareValidationResult
{
    public List<ValidationResult> Errors { get; }

    public SmartSoftwareValidationResult()
    {
        Errors = new List<ValidationResult>();
    }
}
