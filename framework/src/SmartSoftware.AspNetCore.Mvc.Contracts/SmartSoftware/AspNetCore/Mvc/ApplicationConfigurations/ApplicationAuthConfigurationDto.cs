using System;
using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

[Serializable]
public class ApplicationAuthConfigurationDto
{
    public Dictionary<string, bool> GrantedPolicies { get; set; }

    public ApplicationAuthConfigurationDto()
    {
        GrantedPolicies = new Dictionary<string, bool>();
    }
}
