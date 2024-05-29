using System;
using System.Collections.Generic;

namespace SmartSoftware.Http.ProxyScripting.Configuration;

public class SmartSoftwareApiProxyScriptingOptions
{
    public IDictionary<string, Type> Generators { get; }

    public SmartSoftwareApiProxyScriptingOptions()
    {
        Generators = new Dictionary<string, Type>();
    }
}
