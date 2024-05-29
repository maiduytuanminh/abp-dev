using System;
using System.Collections.Generic;

namespace SmartSoftware.Http.Client.StaticProxying;

public class SmartSoftwareHttpClientStaticProxyingOptions
{
    public List<Type> BindingFromQueryTypes { get; }

    public SmartSoftwareHttpClientStaticProxyingOptions()
    {
        BindingFromQueryTypes = new List<Type>();
    }
}
