using System;
using System.Collections.Generic;

namespace SmartSoftware.Cli.ServiceProxying;

public class SmartSoftwareCliServiceProxyOptions
{
    public IDictionary<string, Type> Generators { get; }

    public SmartSoftwareCliServiceProxyOptions()
    {
        Generators = new Dictionary<string, Type>();
    }
}
