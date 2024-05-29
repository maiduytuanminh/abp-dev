using System.Collections.Generic;
using JetBrains.Annotations;

namespace SmartSoftware.Validation.StringValues;

public interface IStringValueType
{
    string Name { get; }

    object? this[string key] { get; set; }

    [NotNull]
    Dictionary<string, object?> Properties { get; }

    IValueValidator Validator { get; set; }
}
