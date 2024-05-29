using System;

namespace SmartSoftware.Http.Client.ClientProxying;

public interface ICurrentApiVersionInfo
{
    ApiVersionInfo? ApiVersionInfo { get; }

    IDisposable Change(ApiVersionInfo? apiVersionInfo);
}
