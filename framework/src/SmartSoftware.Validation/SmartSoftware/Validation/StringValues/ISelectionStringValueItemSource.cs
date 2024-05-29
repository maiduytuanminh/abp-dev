using System.Collections.Generic;

namespace SmartSoftware.Validation.StringValues;

public interface ISelectionStringValueItemSource
{
    ICollection<ISelectionStringValueItem> Items { get; }
}
