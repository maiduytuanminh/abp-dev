using System;
using System.Reflection;

namespace SmartSoftware.Http.ProxyScripting.Configuration;

public static class SmartSoftwareApiProxyScriptingConfiguration
{
    public static Func<PropertyInfo, string?> PropertyNameGenerator { get; set; }

    static SmartSoftwareApiProxyScriptingConfiguration()
    {
        PropertyNameGenerator = propertyInfo =>
            propertyInfo.GetSingleAttributeOrNull<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name;
    }
}
