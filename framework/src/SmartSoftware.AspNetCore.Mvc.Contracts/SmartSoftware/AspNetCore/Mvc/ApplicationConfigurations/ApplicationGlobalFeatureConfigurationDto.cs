using System;
using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

[Serializable]
public class ApplicationGlobalFeatureConfigurationDto
{
    public HashSet<string> EnabledFeatures { get; set; }

    public ApplicationGlobalFeatureConfigurationDto()
    {
        EnabledFeatures = new HashSet<string>();
    }
}
