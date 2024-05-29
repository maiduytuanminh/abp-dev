using System;

namespace SmartSoftware.AspNetCore.SignalR;

public interface ISmartSoftwareHubContextAccessor
{
    SmartSoftwareHubContext Context { get; }

    IDisposable Change(SmartSoftwareHubContext context);
}

