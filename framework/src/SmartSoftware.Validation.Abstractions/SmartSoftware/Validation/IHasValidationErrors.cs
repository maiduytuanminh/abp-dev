using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.Validation;

public interface IHasValidationErrors
{
    IList<ValidationResult> ValidationErrors { get; }
}
