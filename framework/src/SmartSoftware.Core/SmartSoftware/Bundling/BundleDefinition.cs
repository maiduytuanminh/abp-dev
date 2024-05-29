﻿using System.Collections.Generic;

namespace SmartSoftware.Bundling;

public class BundleDefinition
{
    public string Source { get; set; } = default!;

    public Dictionary<string, string> AdditionalProperties { get; set; }

    public bool ExcludeFromBundle { get; set; }

    public BundleDefinition()
    {
        AdditionalProperties = new Dictionary<string, string>();
    }
}
