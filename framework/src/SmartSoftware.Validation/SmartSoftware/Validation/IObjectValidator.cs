using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartSoftware.Validation;

public interface IObjectValidator
{
    Task ValidateAsync(
        object validatingObject,
        string? name = null,
        bool allowNull = false
    );

    Task<List<ValidationResult>> GetErrorsAsync(
        object validatingObject,
        string? name = null,
        bool allowNull = false
    );
}
