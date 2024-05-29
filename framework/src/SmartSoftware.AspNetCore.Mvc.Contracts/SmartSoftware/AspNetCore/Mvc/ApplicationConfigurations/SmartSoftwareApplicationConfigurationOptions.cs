using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public class SmartSoftwareApplicationConfigurationOptions
{
    public List<IApplicationConfigurationContributor> Contributors { get; }

    public SmartSoftwareApplicationConfigurationOptions()
    {
        Contributors = new List<IApplicationConfigurationContributor>();
    }
}
