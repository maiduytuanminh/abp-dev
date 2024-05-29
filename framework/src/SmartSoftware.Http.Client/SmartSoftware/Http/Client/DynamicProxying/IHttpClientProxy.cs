﻿namespace SmartSoftware.Http.Client.DynamicProxying;

public interface IHttpClientProxy<out TRemoteService>
{
    TRemoteService Service { get; }
}
