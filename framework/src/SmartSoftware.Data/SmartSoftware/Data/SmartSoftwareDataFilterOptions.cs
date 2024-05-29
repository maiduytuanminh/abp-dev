using System;
using System.Collections.Generic;

namespace SmartSoftware.Data;

public class SmartSoftwareDataFilterOptions
{
    public Dictionary<Type, DataFilterState> DefaultStates { get; }

    public SmartSoftwareDataFilterOptions()
    {
        DefaultStates = new Dictionary<Type, DataFilterState>();
    }
}
