using System;
using System.Net.Http;

namespace SmartSoftware.IdentityModel;

public class IdentityModelHttpRequestMessageOptions
{
    public Action<HttpRequestMessage>? ConfigureHttpRequestMessage { get; set; }
}
