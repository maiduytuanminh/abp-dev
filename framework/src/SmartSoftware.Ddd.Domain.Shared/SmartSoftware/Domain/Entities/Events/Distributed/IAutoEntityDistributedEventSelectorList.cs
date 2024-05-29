using System;
using System.Collections.Generic;

namespace SmartSoftware.Domain.Entities.Events.Distributed;

public interface IAutoEntityDistributedEventSelectorList : IList<NamedTypeSelector>
{
}
