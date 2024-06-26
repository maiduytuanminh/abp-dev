﻿using System.Collections.Generic;
using SmartSoftware.Collections;

namespace SmartSoftware.FeatureManagement;

public class FeatureManagementOptions
{
    public ITypeList<IFeatureManagementProvider> Providers { get; }

    public Dictionary<string, string> ProviderPolicies { get; }

    /// <summary>
    /// Default: true.
    /// </summary>
    public bool SaveStaticFeaturesToDatabase { get; set; } = true;

    /// <summary>
    /// Default: false.
    /// </summary>
    public bool IsDynamicFeatureStoreEnabled { get; set; }

    public FeatureManagementOptions()
    {
        Providers = new TypeList<IFeatureManagementProvider>();
        ProviderPolicies = new Dictionary<string, string>();
    }
}
