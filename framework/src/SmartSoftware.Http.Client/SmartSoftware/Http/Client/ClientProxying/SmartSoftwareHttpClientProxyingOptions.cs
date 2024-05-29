using System;
using System.Collections.Generic;

namespace SmartSoftware.Http.Client.ClientProxying;

public class SmartSoftwareHttpClientProxyingOptions
{
    public Dictionary<Type, Type> QueryStringConverts { get; set; }

    public Dictionary<Type, Type> FormDataConverts { get; set; }

    public Dictionary<Type, Type> PathConverts { get; set; }

    public SmartSoftwareHttpClientProxyingOptions()
    {
        QueryStringConverts = new Dictionary<Type, Type>();
        FormDataConverts = new Dictionary<Type, Type>();
        PathConverts = new Dictionary<Type, Type>();
    }
}
