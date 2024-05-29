using System;

namespace SmartSoftware.Http.Client.Proxying;

public class HttpClientProxyConfig
{
    public Type Type { get; }

    public string RemoteServiceName { get; }

    public HttpClientProxyConfig(Type type, string remoteServiceName)
    {
        Type = type;
        RemoteServiceName = remoteServiceName;
    }
}
