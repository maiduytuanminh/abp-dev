using System;
using System.Collections.Generic;
using SmartSoftware.Http.Client.DynamicProxying;
using SmartSoftware.Http.Client.Proxying;

namespace SmartSoftware.Http.Client;

public class SmartSoftwareHttpClientOptions
{
    public Dictionary<Type, HttpClientProxyConfig> HttpClientProxies { get; set; }

    public SmartSoftwareHttpClientOptions()
    {
        HttpClientProxies = new Dictionary<Type, HttpClientProxyConfig>();
    }
}
